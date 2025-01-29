using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Species.Application.Queries.SpeciesAndBreedExists;

public class SpeciesAndBreedExistsHandler 
    : IQueryHandler<UnitResult<Error>, SpeciesAndBreedExistQuery>
{
    private readonly IReadDbContext _readDbContext;

    public SpeciesAndBreedExistsHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<UnitResult<Error>> Handle(
        SpeciesAndBreedExistQuery query, CancellationToken cancellationToken = default)
    {
        if (await _readDbContext.Species.AnyAsync(
            s => s.Id == (Guid)query.SpeciesId,
            cancellationToken)
            == false)
            return Errors.Species.NonExistantSpecies(query.SpeciesId);

        if (await _readDbContext.Breeds.AnyAsync
            (b => b.Id == query.BreedId,
            cancellationToken) 
            == false)
            return Errors.Species.NonExistantBreed(query.BreedId);

        return UnitResult.Success<Error>();
    }
}
