using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects.Breed;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Species;

namespace AnimalVolunteer.Domain.Entities;

public class Breed : Entity<BreedId>
{
    // EF Core ctor
    private Breed(BreedId id) : base(id) { }
    public Title Title { get; private set; } = null!;
    public SpeciesId SpeciesId { get; private set; } = null!;
}
