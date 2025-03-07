﻿using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Volunteers.Domain.Enums;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    string Name,
    string Description,
    string Color,
    double Weight,
    double Height,
    Guid SpeciesId,
    Guid BreedId,
    string HealthDescription,
    bool IsVaccinated,
    bool IsNeutered,
    string Country,
    string City,
    string Street,
    string? House,
    DateOnly BirthDate,
    CurrentStatus CurrentStatus) : ICommand;