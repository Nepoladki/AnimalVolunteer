using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Root;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using System.Net;
using System.Xml.Linq;

namespace AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Entities;

public sealed class Pet : Common.Entity<PetId>
{
    // EF Core ctor
    private Pet(PetId id) : base(id) { }
    private Pet(
        PetId id,
        Name name,
        Description description,
        PhysicalParameters physicalParameters,
        SpeciesAndBreed speciesAndBreed,
        HealthInfo healthInfo,
        Address address,
        DateOnly birthDate,
        CurrentStatus currentStatus,
        ValueObjectList<ContactInfo> contactInfos,
        ValueObjectList<PaymentDetails> paymentDetails,
        ValueObjectList<PetPhoto> photos) : base(id)
    {
        Name = name;
        Description = description;
        PhysicalParameters = physicalParameters;
        SpeciesAndBreed = speciesAndBreed;
        HealthInfo = healthInfo;
        Address = address;
        ContactInfoList = contactInfos;
        BirthDate = birthDate;
        CurrentStatus = currentStatus;
        CreatedAt = DateTime.UtcNow;
        PaymentDetailsList = paymentDetails;
        PetPhotosList = photos;
    }

    private bool _isDeleted;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public PhysicalParameters PhysicalParameters { get; private set; } = null!;
    public SpeciesAndBreed SpeciesAndBreed { get; private set; } = null!;
    public HealthInfo HealthInfo { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public Position Position { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; } = default!;
    public CurrentStatus CurrentStatus { get; private set; }
    public DateTime CreatedAt { get; private set; } = default!;
    public IReadOnlyList<ContactInfo> ContactInfoList { get; private set; } = null!;
    public IReadOnlyList<PaymentDetails> PaymentDetailsList { get; private set; } = null!;
    public IReadOnlyList<PetPhoto> PetPhotosList { get; private set; } = null!;
    public VolunteerId VolunteerId { get; private set; } = null!;
    
    public static Pet InitialCreate(
        PetId id,
        Name name,
        Description description,
        PhysicalParameters physicalParameters,
        SpeciesAndBreed speciesAndBreed,
        HealthInfo healthInfo,
        Address address,
        DateOnly birthDate,
        CurrentStatus currentStatus)
    {
        var contactInfos = new ValueObjectList<ContactInfo>([]);
        var paymentDetails = new ValueObjectList<PaymentDetails>([]);
        var photos = new ValueObjectList<PetPhoto>([]);

        return new Pet(
            id,
            name,
            description,
            physicalParameters,
            speciesAndBreed,
            healthInfo, 
            address,
            birthDate,
            currentStatus,
            contactInfos,
            paymentDetails,
            photos);
    }

    internal void SoftDelete() => _isDeleted = true;

    internal void Restore() => _isDeleted = false;

    internal void UpdatePhotos(ValueObjectList<PetPhoto> photos) =>
        PetPhotosList = photos;

    internal void DeleteAllPhotos() => PetPhotosList = [];

    internal UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    internal UnitResult<Error> MoveBackward()
    {
        var newPosition = Position.Backward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    internal void MoveToPosition(Position position)
    {
        Position = position;
    }

    internal void ChangeStatus(CurrentStatus newStatus)
    {
        CurrentStatus = newStatus;
    }
    
    internal void UpdatePet(
        Name name,
        Description description,
        PhysicalParameters physicalParameters,
        SpeciesAndBreed speciesAndBreed,
        HealthInfo healthInfo,
        Address address,
        DateOnly birthDate,
        CurrentStatus currentStatus,
        ValueObjectList<ContactInfo> contactInfos,
        ValueObjectList<PaymentDetails> paymentDetails)
    {
        Name = name;
        Description = description;
        PhysicalParameters = physicalParameters;
        SpeciesAndBreed = speciesAndBreed;
        HealthInfo = healthInfo;
        Address = address;
        BirthDate = birthDate;
        CurrentStatus = currentStatus;
        ContactInfoList = contactInfos;
        PaymentDetailsList = paymentDetails;
    }
}