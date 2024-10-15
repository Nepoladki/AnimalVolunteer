using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.PetType.Entities;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Aggregates.SpeciesManagement.Root;
using AnimalVolunteer.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Species.Infrastructure;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _writeDbContext;

    public SpeciesRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<Species?> GetSpeciesById(
        SpeciesId Id, CancellationToken cancellationToken)
    {
        return await _writeDbContext.Species.Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == Id, cancellationToken);
    }

    public void DeleteSpecies(Species species)
    {
        _writeDbContext.Species.Remove(species);
    }
}
