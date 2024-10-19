using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using DomainEntity = AnimalVolunteer.Species.Domain.Root;

namespace AnimalVolunteer.Species.Application;

public interface ISpeciesRepository
{
    Task<DomainEntity.Species?> GetSpeciesById(SpeciesId Id, CancellationToken cancellationToken);
    void DeleteSpecies(DomainEntity.Species species);
}