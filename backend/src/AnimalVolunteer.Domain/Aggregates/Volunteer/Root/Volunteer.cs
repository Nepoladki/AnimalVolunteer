using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.Root;

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
        ValueObjectList<ContactInfo> contactInfoList,
        ValueObjectList<SocialNetwork> socialNetworkList,
        ValueObjectList<PaymentDetails> paymentDetailsList) : base(id)
    {
        _pets = [];
        FullName = fullName;
        Email = email;
        Description = description;
        Statistics = statistics;
        ContactInfoList = contactInfoList;
        SocialNetworksList = socialNetworkList;
        PaymentDetailsList = paymentDetailsList;
    }

    private readonly List<Pet> _pets = default!;

    private bool _isDeleted;
    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public Statistics Statistics { get; private set; } = null!;
    public IReadOnlyList<ContactInfo> ContactInfoList { get; private set; } = null!;
    public IReadOnlyList<SocialNetwork> SocialNetworksList { get; private set; } = null!;
    public IReadOnlyList<PaymentDetails> PaymentDetailsList { get; private set; } = null!;
    public IReadOnlyList<Pet> Pets => _pets;
    public int CountPetsFoundedHome() => Statistics.PetsFoundedHome;
    public int CountPetsLookingForHome() => Statistics.PetsLookingForHome;
    public int CountPetsInVetClinic() => Statistics.PetsInVetClinic;

    public static Volunteer Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        Statistics statistics,
        ValueObjectList<SocialNetwork> socialNetworkList,
        ValueObjectList<PaymentDetails> paymentDetailsList)
    {
        var contactInfoList = new List<ContactInfo>();

        return new(
        id,
        fullName,
        email,
        description,
        statistics,
        contactInfoList,
        socialNetworkList,
        paymentDetailsList);
    }
    public void UpdateMainInfo(FullName fullName, Email email, Description description)
    {
        FullName = fullName;
        Email = email;
        Description = description;
    }

    public void UpdateContactInfo(ValueObjectList<ContactInfo> contacts) => 
        ContactInfoList = contacts;

    public void UpdateSocialNetworks(ValueObjectList<SocialNetwork> socialNetworks) => 
        SocialNetworksList = socialNetworks;

    public void UpdatePaymentDetails(ValueObjectList<PaymentDetails> paymentDetails) => 
        PaymentDetailsList = paymentDetails;

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

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);

        if (pet is null)
            return Errors.General.NotFound(petId);

        return pet;
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var position = Position.Create(_pets.Count + 1);
        if (position.IsFailure)
            return position.Error;

        pet.UpdatePosition(position.Value);

        _pets.Add(pet);

        return Result.Success<Error>();
    }

    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition || _pets.Count == 1)
            return Result.Success<Error>();

        var ajustedPosition = AjustPositionIfOutOfRange(newPosition);
        if (ajustedPosition.IsFailure)
            return ajustedPosition.Error;

        newPosition = ajustedPosition.Value;

        var arrangeResult = ArrangePets(newPosition, currentPosition);
        if (arrangeResult.IsFailure)
            return arrangeResult.Error;

        pet.MoveToPosition(newPosition);

        return Result.Success<Error>();
    }

    private Result<Position, Error> AjustPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
    }
    private UnitResult<Error> ArrangePets(Position newPosition, Position currentPosition)
    {
        if (newPosition < currentPosition)
        {
            var petsToMove = _pets
                .Where(x => x.Position >= newPosition && x.Position < currentPosition);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        else //if (newPosition > currentPosition)
        {
            var petsToMove = _pets
                .Where(x => x.Position > currentPosition && x.Position <= newPosition);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBackward();
                if (result.IsFailure)
                    return result.Error;
            }
        }

        return Result.Success<Error>();
    }
}