using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Infrastructure.DbContexts;

namespace AnimalVolunteer.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _writeDbContext;

    public Task DeleteBreed(SpeciesId speciesId, Guid breedId)
    {
        _writeDbContext.Species.Remove();
        _writeDbContext.SaveChanges();
    }

    public Task DeleteSpecies(SpeciesId speciesId)
    {
        throw new NotImplementedException();
    }
}
