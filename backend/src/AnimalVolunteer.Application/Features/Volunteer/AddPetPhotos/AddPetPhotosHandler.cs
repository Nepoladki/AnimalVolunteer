using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;

public class AddPetPhotosHandler
{
    private const string BUCKET_NAME = "photos";
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotosHandler> _logger;

    public AddPetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider fileProvider,
        ILogger<AddPetPhotosHandler> logger,
        IApplicationDbContext dbContext)
    {
        _volunteerRepository = volunteerRepository;
        _fileProvider = fileProvider;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<UnitResult<Error>> Handle(
        AddPetPhotosCommand command, 
        CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.BeginTransaction(cancellationToken);

        var volunteerResult = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        List<FileToUploadDto> files = [];

        try
        {
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);

                var filePathResult = FilePath.Create(Guid.NewGuid(), extension);

                if (filePathResult.IsFailure)
                    return filePathResult.Error;

                var fileToUpload = new FileToUploadDto(
                    filePathResult.Value.Value,
                    file.Content);

                files.Add(fileToUpload);
            }

            // Сохранение в бд

            var uploadResult = await _fileProvider
                .UploadFiles(files, BUCKET_NAME, cancellationToken);

            if (uploadResult.IsFailure)
                return uploadResult.Error;
            
            transaction.Commit();

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occured while uploading pet photos to volunteer with id {id}",
                command.VolunteerId);

            transaction.Rollback();

            return Error.Failure("volunteer.pet.photos.failure",
                "Error occured while uploading pet photos");
        }
    }
}
