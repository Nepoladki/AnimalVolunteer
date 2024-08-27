using AnimalVolunteer.Domain.Aggregates.PetType.Entities;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Domain.Aggregates.PetType;

public sealed class Species : Entity<SpeciesId>
{
    // EF Core ctor
    private Species(SpeciesId id) : base(id) { }
    public Title Title { get; private set; } = null!;
    public List<Breed> Breeds { get; private set; } = [];
}
