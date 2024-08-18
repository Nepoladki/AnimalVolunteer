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

    public static Result<SpeciesAndBreed> Create(Guid speciesId, Guid breedId)
    {
        if (speciesId == Guid.Empty)
            return Result.Failure<SpeciesAndBreed>("Invalid species id");

        if (breedId == Guid.Empty)
            return Result.Failure<SpeciesAndBreed>("Invalid breed id");

        return Result.Success(new SpeciesAndBreed(speciesId, breedId));
    }
}
