using My = AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Species.Domain.Entities;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Species.Domain.Root;

public sealed class Species : My.Entity<SpeciesId>
{
    // EF Core ctor
    private Species(SpeciesId id) : base(id) { }

    private readonly List<Breed> _breeds = null!;
    public Name Name { get; private set; } = null!;
    public IReadOnlyList<Breed> Breeds => _breeds;
    public UnitResult<My.Error> DeleteBreed(Breed breed)
    {
        var deleted = _breeds.Remove(breed);
        if (deleted == false)
            return My.Errors.Species.BreedDeletingError(breed.Id);

        return Result.Success<My.Error>();
    }
}
