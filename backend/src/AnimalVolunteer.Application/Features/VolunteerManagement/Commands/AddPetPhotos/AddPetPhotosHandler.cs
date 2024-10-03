using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPetPhotos;

public class AddPetPhotosHandler : ICommandHandler<AddPetPhotosCommand>
{
    private const string BUCKET_NAME = "photos";
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private readonly IValidator<AddPetPhotosCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfoDto>> _messageQueue;

    public AddPetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        IFileProvider fileProvider,
        ILogger<AddPetPhotosHandler> logger,
        IValidator<AddPetPhotosCommand> validator,
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
        AddPetPhotosCommand command,
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

        var petResult = volunteerResult.Value
            .GetPetById(PetId.CreateWithGuid(command.PetId));
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

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
                .UploadFiles(files, BUCKET_NAME, cancellationToken);
            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(
                    files.Select(x => new FileInfoDto(BUCKET_NAME, x.FilePath.Value)),
                    cancellationToken);

                return uploadResult.Error.ToErrorList();
            }

            var petPhotos = PetPhotoList.Create(
                files.Select(f => PetPhoto.Create(f.FilePath, false).Value).ToList());

            petResult.Value.UpdatePhotos(petPhotos);

            await _unitOfWork.SaveChanges(cancellationToken);

            transaction.Commit();

            return Result.Success<ErrorList>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occured while uploading pet photos to volunteer with id {id}",
                command.VolunteerId);

            transaction.Rollback();

            return Error.Failure("volunteer.pet.photos.failure",
                "Error occured while uploading pet photos").ToErrorList();
        }
    }
}
