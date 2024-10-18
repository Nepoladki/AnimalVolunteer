using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using My = AnimalVolunteer.SharedKernel;

namespace AnimalVolunteer.Species.Domain.Entities;

public class Breed : My.Entity<BreedId>
{
    // EF Core ctor
    private Breed(BreedId id) : base(id) { }
    public Name Name { get; private set; } = null!;
    public SpeciesId SpeciesId { get; private set; } = null!;
}