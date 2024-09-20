using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer;

public sealed class Volunteer : Common.Entity<VolunteerId>
{
    // EF Core ctor
    private Volunteer(VolunteerId id) : base(id) { }
    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        Statistics statistics,
        ContactInfoList contactInfoList,
        SocialNetworkList socialNetworkList,
        PaymentDetailsList paymentDetailsList) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        Statistics = statistics;
        ContactInfos = contactInfoList;
        SocialNetworks = socialNetworkList;
        PaymentDetails = paymentDetailsList;
    }

    private readonly List<Pet> _pets = default!;

    private bool _isDeleted;
    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public Statistics Statistics { get; private set; } = null!;
    public ContactInfoList ContactInfos { get; private set; } = null!;
    public SocialNetworkList SocialNetworks { get; private set; } = null!;
    public PaymentDetailsList PaymentDetails { get; private set; } = null!;
    public IReadOnlyList<Pet> Pets => _pets;
    public int CountPetsFoundedHome() => Statistics.PetsFoundedHome;
    public int CountPetsLookingForHome() => Statistics.PetsLookingForHome;
    public int CountPetsInVetClinic() => Statistics.PetsInVetClinic;

    public void UpdateMainInfo(FullName fullName, Email email, Description description)
    {
        FullName = fullName;
        Email = email;
        Description = description;
    }

    public void UpdateContactInfo(ContactInfoList contacts) => ContactInfos = contacts;

    public void UpdateSocialNetworks(SocialNetworkList socialNetworks) => SocialNetworks = socialNetworks;

    public void UpdatePaymentDetails(PaymentDetailsList paymentDetails) => PaymentDetails = paymentDetails;

    public void Delete()
    {
        _isDeleted = true;

        foreach (var pet in _pets)
        {
            pet.Delete();
        }
    }

    public void Restore()
    {
        _isDeleted = false;

        foreach (var pet in _pets)
        {
            pet.Restore();
        }
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        // валидация

        _pets.Add(pet);

        return Result.Success<Error>();
    }

    public static Volunteer Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        Statistics statistics,
        ContactInfoList contactInfoList,
        SocialNetworkList socialNetworkList,
        PaymentDetailsList paymentDetailsList) =>
            new(
                id,
                fullName,
                email,
                description,
                statistics,
                contactInfoList,
                socialNetworkList,
                paymentDetailsList);
}