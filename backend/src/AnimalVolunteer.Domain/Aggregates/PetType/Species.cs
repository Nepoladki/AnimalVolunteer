using AnimalVolunteer.Domain.Aggregates.PetType.Entities;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.PetType;

public sealed class Species : Common.Entity<SpeciesId>
{
    // EF Core ctor
    private Species(SpeciesId id) : base(id) { }

    private readonly List<Breed> _breeds = null!;
    public Name Name { get; private set; } = null!;
    public IReadOnlyList<Breed> Breeds => _breeds;
    public UnitResult<Error> DeleteBreed(Breed breed)
    {
        var deleted = _breeds.Remove(breed);
        if (deleted == false)
            return Errors.Species.BreedDeletingError(breed.Id);

        return Result.Success<Error>();
    }
}
