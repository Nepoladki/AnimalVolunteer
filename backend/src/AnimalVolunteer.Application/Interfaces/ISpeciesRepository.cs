using AnimalVolunteer.Domain.Aggregates.PetType;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;

namespace AnimalVolunteer.Application.Interfaces;

public interface ISpeciesRepository
{
    Task<Species?> GetSpeciesById(SpeciesId Id, CancellationToken cancellationToken);
    void DeleteSpecies(Species species);
}