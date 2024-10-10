using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.HardDeletePet;

public class HardDeletePetHandler : ICommandHandler<HardDeletePetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<HardDeletePetHandler> _logger;
    private readonly IValidator<HardDeletePetCommand> _validator;

    public HardDeletePetHandler(
        IValidator<HardDeletePetCommand> validator, 
        ILogger<HardDeletePetHandler> logger, 
        IFileProvider fileProvider, 
        IUnitOfWork unitOfWork, 
        IVolunteerRepository volunteerRepository)
    {
        _validator = validator;
        _logger = logger;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        HardDeletePetCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteerResult = await _volunteerRepository
            .GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.CreateWithGuid(command.PetId);

        var deletingResult = volunteerResult.Value.HardDeletePet(petId);
        if (deletingResult.IsFailure)
        {
            _logger.LogError("Error occured while deleting volunteer's (id = {vId}) pet (id = {pId})", 
                volunteerId, 
                petId);
            return deletingResult.Error.ToErrorList();
        }

        var filePathsToDelete = deletingResult.Value;

        foreach (var filePath in filePathsToDelete)
        {
            var fileInfo = new FileInfoDto(Constants.MINIO_BUCKET_NAME, filePath);

            var fileDeletingResult = await _fileProvider
                .DeleteFile(fileInfo, cancellationToken);

            if (fileDeletingResult.IsFailure)
            {
                _logger.LogError("Error occured while deleting file with name {name} from storage",
                    filePath);
            }
        }

        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
