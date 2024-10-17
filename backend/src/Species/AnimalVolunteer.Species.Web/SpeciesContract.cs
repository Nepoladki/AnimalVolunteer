using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Species.Application.Queries.SpeciesAndBreedExists;
using AnimalVolunteer.Species.Contracts;
using AnimalVolunteer.Species.Contracts.Requests;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Species.Web;

public class SpeciesContract : ISpeciesContract
{
    private readonly SpeciesAndBreedExistsHandler _speciesAndBreedExistsHandler;

    public SpeciesContract(SpeciesAndBreedExistsHandler speciesAndBreedExistsHandler)
    {
        _speciesAndBreedExistsHandler = speciesAndBreedExistsHandler;
    }

    public async Task<UnitResult<Error>> SpeciesAndBreedExists(
        SpeciesAndBreedExistRequest request, CancellationToken cancellationToken = default)
    {
        var query = new SpeciesAndBreedExistQuery(request.SpeciesId, request.BreedId);

        return await _speciesAndBreedExistsHandler.Handle(query, cancellationToken);
    }
}
