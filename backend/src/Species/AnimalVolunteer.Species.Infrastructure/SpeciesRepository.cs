using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Species.Application;
using AnimalVolunteer.Species.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using DomainEntity = AnimalVolunteer.Species.Domain.Root;

namespace AnimalVolunteer.Species.Infrastructure;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _writeDbContext;

    public SpeciesRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<DomainEntity.Species?> GetSpeciesById(
        SpeciesId Id, CancellationToken cancellationToken)
    {
        return await _writeDbContext.Species.Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == Id, cancellationToken);
    }

    public void DeleteSpecies(DomainEntity.Species species)
    {
        _writeDbContext.Species.Remove(species);
    }
}
