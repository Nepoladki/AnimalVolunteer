using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Entities;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Volunteer;

namespace AnimalVolunteer.Domain.Aggregates;

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
    public static Volunteer Create(
        FullName fullName, 
        string description, 
        int expirienceYears, 
        int petsFoundedHome,
        int petsLookingForHome, 
        int petsInVetClinic,
        ContactInfoList contacts,
        SocialNetworkList socialNetworks,
        PaymentDetailsList paymentDetails)
    {
        return new Volunteer(VolunteerId.Create())
        {
            FullName = fullName,
            Description = description,
            ExpirienceYears = expirienceYears,
            PetsFoundedHome = petsFoundedHome,
            PetsLookingForHome = petsLookingForHome,
            PetsInVetClinic = petsInVetClinic,
            ContactInfos = contacts,
            SocialNetworks = socialNetworks,
            PaymentDetails = paymentDetails,
        };
    }
}
