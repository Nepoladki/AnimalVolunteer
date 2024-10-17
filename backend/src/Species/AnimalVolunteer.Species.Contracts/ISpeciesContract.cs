using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Species.Contracts.Requests;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Species.Contracts;

public interface ISpeciesContract
{
    public Task<UnitResult<Error>> SpeciesAndBreedExists(
        SpeciesAndBreedExistRequest request, CancellationToken cancellationToken = default);
}
