using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Volunteers.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.AnyPetExistsByBreed;

public class AnyPetExistsByBreedHandler : IQueryHandler<bool, AnyPetExistsByBreedQuery>
{
    private readonly IReadDbContext _readDbContext;

    public AnyPetExistsByBreedHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<bool> Handle(
        AnyPetExistsByBreedQuery query, CancellationToken cancellationToken = default)
    {
        return await _readDbContext.Pets.AnyAsync(p => p.BreedId == query.BreedId, cancellationToken);
    }
}
