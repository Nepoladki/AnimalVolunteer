using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects;

namespace AnimalVolunteer.Domain.Entities;

public sealed class Volunteer : Entity<VolunteerId>
{
    // EF Core ctor
    private Volunteer(VolunteerId id) : base(id) { }
    public FullName FullName { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int ExpirienceYears { get; private set; }
    public int PetsFoundedHome { get; private set; }
    public int PetsLookingForHome { get; private set; }
    public int PetsInVetClinic { get; private set; }
    public ContactInfoList ContactInfos { get; private set; } = null!;
    public SocialNetworkList SocialNetworks { get; private set; } = null!;
    public PaymentDetailsList PaymentDetails { get; private set; } = null!;
    public List<Pet> Pets { get; private set; } = [];
}
