using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Core;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Volunteers.Application.Interfaces;
using AnimalVolunteer.Core.Abstractions;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.DeletePetPhotos;

public class DeletePetPhotosHandler : ICommandHandler<DeletePetPhotosCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    private readonly IValidator<DeletePetPhotosCommand> _validator;

    public DeletePetPhotosHandler(
        IReadDbContext readDbContext,
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        ILogger<DeletePetPhotosHandler> logger,
        IValidator<DeletePetPhotosCommand> validator)
    {
        _readDbContext = readDbContext;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeletePetPhotosCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var petId = PetId.CreateWithGuid(command.PetId);

        var petExists = await _readDbContext.Pets
            .AnyAsync(p => p.Id == (Guid)petId, cancellationToken);
        if (petExists == false)
            return Errors.General.NotFound(petId).ToErrorList();

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteerResult = await _volunteerRepository
            .GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var deleteResult = volunteer.DeletePetPhotos(petId);
        if (deleteResult.IsFailure)
            return deleteResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Successfully deleted all pet photos of pet with id {petId}",
            petId);

        return Result.Success<ErrorList>();
    }
}
