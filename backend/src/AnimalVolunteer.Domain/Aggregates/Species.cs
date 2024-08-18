using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Entities;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Species;

namespace AnimalVolunteer.Domain.Aggregates;

public sealed class Species : Entity<SpeciesId>
{
    // EF Core ctor
    private Species(SpeciesId id) : base(id) { }
    public Title Title { get; private set; } = null!;
    public List<Breed> Breeds { get; private set; } = [];
}
