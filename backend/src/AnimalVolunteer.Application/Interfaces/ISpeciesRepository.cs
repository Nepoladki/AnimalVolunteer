using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;

namespace AnimalVolunteer.Application.Interfaces;

public interface ISpeciesRepository
{
    Task DeleteBreed(SpeciesId speciesId, Guid breedId);
    Task DeleteSpecies(SpeciesId speciesId);
}