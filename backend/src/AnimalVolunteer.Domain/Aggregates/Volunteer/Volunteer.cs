using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer;

public sealed class Volunteer : Common.Entity<VolunteerId>
{
    // EF Core ctor
    private Volunteer(VolunteerId id) : base(id) { }
    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Description description,
        Statistics statistics) : base(id)
    {
        FullName = fullName;
        Description = description;
        Statistics = statistics;
    }
    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Description description,
        Statistics statistics,
        ContactInfoList contactInfoList,
        SocialNetworkList socialNetworkList,
        PaymentDetailsList paymentDetailsList) : base(id)
    {
        FullName = fullName;
        Description = description;
        Statistics = statistics;
        ContactInfos = contactInfoList;
        SocialNetworks = socialNetworkList;
        PaymentDetails = paymentDetailsList;
    }
    public FullName FullName { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public Statistics Statistics { get; private set; } = null!;
    public ContactInfoList ContactInfos { get; private set; } = null!;
    public SocialNetworkList SocialNetworks { get; private set; } = null!;
    public PaymentDetailsList PaymentDetails { get; private set; } = null!;
    public List<Pet> Pets { get; private set; } = [];
    public int CountPetsFoundedHome() => Statistics.PetsFoundedHome;
    public int CountPetsLookingForHome() => Statistics.PetsLookingForHome;
    public int CountPetsInVetClinic() => Statistics.PetsInVetClinic;
    public static Volunteer Create(
        VolunteerId id,
        FullName fullName,
        Description description,
        Statistics statistics) => new(id, fullName, description, statistics);

    public static Volunteer CreateWithLists(
        VolunteerId id,
        FullName fullName,
        Description description,
        Statistics statistics,
        ContactInfoList contactInfoList,
        SocialNetworkList socialNetworkList,
        PaymentDetailsList paymentDetailsList)
        => new(
            id,
            fullName,
            description,
            statistics,
            contactInfoList,
            socialNetworkList,
            paymentDetailsList);
}
