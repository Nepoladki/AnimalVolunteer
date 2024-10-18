using AnimalVolunteer.Core.Abstractions.CQRS;
using Microsoft.EntityFrameworkCore;
namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.AnyPetExistsBySpecies;


public class AnyPetExistsBySpeciesHandler : IQueryHandler<bool, AnyPetExistsBySpeciesQuery>
{
    private readonly IReadDbContext _readDbContext;

    public AnyPetExistsBySpeciesHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<bool> Handle(
        AnyPetExistsBySpeciesQuery query, CancellationToken cancellationToken = default)
    {
        return await _readDbContext.Pets.AnyAsync(p => p.SpeciesId == (Guid)query.SpeciesId, cancellationToken);
    }
}

