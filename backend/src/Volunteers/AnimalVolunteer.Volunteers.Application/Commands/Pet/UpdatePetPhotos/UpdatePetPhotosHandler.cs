using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.Volunteers.Application;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePetPhotos;

public class UpdatePetPhotosHandler : ICommandHandler<UpdatePetPhotosCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<UpdatePetPhotosHandler> _logger;
    private readonly IValidator<UpdatePetPhotosCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfoDto>> _messageQueue;

    public UpdatePetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        IFileProvider fileProvider,
        ILogger<UpdatePetPhotosHandler> logger,
        IValidator<UpdatePetPhotosCommand> validator,
        IMessageQueue<IEnumerable<FileInfoDto>> messageQueue)
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _fileProvider = fileProvider;
        _logger = logger;
        _validator = validator;
        _messageQueue = messageQueue;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        UpdatePetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        var volunteerResult = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var petId = PetId.CreateWithGuid(command.PetId);

        List<UploadingFileDto> files = [];

        try
        {
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);

                var filePathResult = FilePath.Create(Guid.NewGuid(), extension);

                if (filePathResult.IsFailure)
                    return filePathResult.Error.ToErrorList();

                var fileToUpload = new UploadingFileDto(
                    filePathResult.Value,
                    file.Content);

                files.Add(fileToUpload);
            }

            var uploadResult = await _fileProvider
                .UploadFiles(files, Constants.MINIO_BUCKET_NAME, cancellationToken);
            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(
                    files.Select(x => new FileInfoDto(
                        Constants.MINIO_BUCKET_NAME,
                        x.FilePath.Value)),
                    cancellationToken);

                return uploadResult.Error.ToErrorList();
            }

            var petPhotos = files.Select(f => PetPhoto
                .Create(f.FilePath, false).Value).ToList();

            var updateResult = volunteer.UpdatePetPhotos(petId, petPhotos);
            if (updateResult.IsFailure)
                return updateResult.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            transaction.Commit();

            _logger.LogInformation(
                "Succsessfully updated pet photos, pet ID = {petId}",
                petId);

            return Result.Success<ErrorList>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occured while uploading pet (id = {petId}) photos to volunteer with id {id}",
                petId,
                command.VolunteerId);

            transaction.Rollback();

            return Error.Failure("volunteer.pet.photos.failure",
                "Error occured while uploading pet photos").ToErrorList();
        }
    }
}
