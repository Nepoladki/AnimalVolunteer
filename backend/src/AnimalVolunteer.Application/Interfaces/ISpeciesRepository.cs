﻿using AnimalVolunteer.Domain.Aggregates.PetType.Entities;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Aggregates.SpeciesManagement.Root;

namespace AnimalVolunteer.Application.Interfaces;

public interface ISpeciesRepository
{
    Task<Species?> GetSpeciesById(SpeciesId Id, CancellationToken cancellationToken);
    void DeleteSpecies(Species species);
}