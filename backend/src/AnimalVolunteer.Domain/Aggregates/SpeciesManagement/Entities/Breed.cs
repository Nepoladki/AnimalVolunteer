using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Domain.Aggregates.PetType.Entities;

public class Breed : Entity<BreedId>
{
    // EF Core ctor
    private Breed(BreedId id) : base(id) { }
    public Name Name { get; private set; } = null!;
}
