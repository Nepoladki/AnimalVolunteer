using AnimalVolunteer.Core.DTOs.Species;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Species.Contracts;

public interface ISpeciesContract
{
    public Task<Result<SpeciesDto, Error>> GetSpeciesById(
        Guid speciesId, CancellationToken cancellationToken);
}
