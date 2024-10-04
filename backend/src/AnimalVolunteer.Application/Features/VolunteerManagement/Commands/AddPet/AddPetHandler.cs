using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<AddPetHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<AddPetCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create();

        var name = Name.Create(command.Name).Value;

        var description = Description.Create(
            command.Description).Value;

        var physicalParamaters = PhysicalParameters.Create(
            command.Color,
            command.Weight,
            command.Height).Value;

        var speciesAndBreed = SpeciesAndBreed
            .Create(command.SpeciesId, command.BreedId).Value;

        var healthInfo = HealthInfo.Create(
            command.HealthDescription,
            command.IsVaccinated,
            command.IsNeutered).Value;

        var address = Address.Create(
            command.Country,
            command.City,
            command.Street,
            command.House).Value;

        var status = command.CurrentStatus;

        var pet = Pet.InitialCreate(
            petId,
            name,
            description,
            physicalParamaters,
            speciesAndBreed,
            healthInfo,
            address,
            command.BirthDate,
            status);

        volunteerResult.Value.AddPet(pet);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Added pet with id {petId} to Volunteer with id {volunteerId}",
            petId,
            volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;

    }
}
