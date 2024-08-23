using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.ValueObjects.Pet;

public record SpeciesAndBreed
{
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }
    private SpeciesAndBreed(Guid speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public static Result<SpeciesAndBreed, Error> Create(Guid speciesId, Guid breedId)
    {
        if (speciesId == Guid.Empty)
            return Errors.General.InvalidValue(nameof(speciesId));

        if (breedId == Guid.Empty)
            return Errors.General.InvalidValue(nameof(breedId));

        return new SpeciesAndBreed(speciesId, breedId);
    }
}
