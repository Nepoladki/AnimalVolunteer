﻿using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Core;
using DomainEntity = AnimalVolunteer.Volunteers.Domain.Entities;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<AddPetHandler> logger,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<AddPetCommand> validator,
        IReadDbContext readDbContext)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<Result<Guid, SharedKernel.ErrorList>> Handle(
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

        var speciesExists = await _readDbContext.Species
            .AnyAsync(s => s.Id == command.SpeciesId, cancellationToken);
        if (speciesExists == false)
        {
            _logger.LogInformation("Tried to create pet with unexisting species id");
            return Errors.Pet.NonExistantSpecies(command.SpeciesId).ToErrorList();
        }

        var breedExists = await _readDbContext.Breeds
            .AnyAsync(b => b.Id == command.BreedId, cancellationToken);
        if (breedExists == false)
        {
            _logger.LogInformation("Tried to create pet with unexisting breed id");
            return Errors.Pet.NonExistantBreed(command.BreedId).ToErrorList();
        }

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

        var pet = DomainEntity.Pet.InitialCreate(
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