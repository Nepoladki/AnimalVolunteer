using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;

public record SpeciesAndBreed
{
    public SpeciesId SpeciesId { get; }
    public Guid BreedId { get; }
    private SpeciesAndBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public static Result<SpeciesAndBreed, Error> Create(SpeciesId speciesId, Guid breedId)
    {
        if (speciesId.Value == Guid.Empty)
            return Errors.General.InvalidValue(nameof(speciesId));

        if (breedId == Guid.Empty)
            return Errors.General.InvalidValue(nameof(breedId));

        return new SpeciesAndBreed(speciesId, breedId);
    }
}
