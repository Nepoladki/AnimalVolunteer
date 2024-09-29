using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;

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
        ContactInfoList contactInfos,
        DateOnly birthDate,
        CurrentStatus currentStatus,
        PaymentDetailsList paymentDetailsList,
        PetPhotoList photos) : base(id)
    {
        Name = name;
        Description = description;
        PhysicalParameters = physicalParameters;
        SpeciesAndBreed = speciesAndBreed;
        HealthInfo = healthInfo;
        Address = address;
        ContactInfos = contactInfos;
        BirthDate = birthDate;
        CurrentStatus = currentStatus;
        CreatedAt = DateTime.UtcNow;
        PaymentDetails = paymentDetailsList;
        PetPhotos = photos;
    }

    private bool _isDeleted;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public PhysicalParameters PhysicalParameters { get; private set; } = null!;
    public SpeciesAndBreed SpeciesAndBreed { get; private set; } = null!;
    public HealthInfo HealthInfo { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public ContactInfoList ContactInfos { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; } = default!;
    public CurrentStatus CurrentStatus { get; private set; }
    public DateTime CreatedAt { get; private set; } = default!;
    public PaymentDetailsList PaymentDetails { get; private set; } = null!;
    public PetPhotoList PetPhotos { get; private set; } = null!;
    public Position Position { get; private set; } = null!;
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
        var contactInfo = ContactInfoList.CreateEmpty();
        var paymentDetails = PaymentDetailsList.CreateEmpty();
        var photos = PetPhotoList.CreateEmpty();

        return new Pet(
            id,
            name,
            description,
            physicalParameters,
            speciesAndBreed,
            healthInfo, address,
            contactInfo,
            birthDate,
            currentStatus,
            paymentDetails,
            photos);
    }
    public void Delete() => _isDeleted = true;
    public void Restore() => _isDeleted = false;
    public void UpdatePhotos(PetPhotoList photos) =>
        PetPhotos = photos;
    public void UpdatePosition(Position number)
        => Position = number;
    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveBackward()
    {
        var newPosition = Position.Backward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public void MoveToPosition(Position position)
    {
        Position = position;
    }
    
}