using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common.ValueObjects;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;

public sealed class Pet : Common.Entity<PetId>
{
    // EF Core ctor
    private Pet(PetId id) : base(id) { }
    private Pet(
        PetId id,
        NameAndDescription nameAndDescription,
        PhysicalParameters physicalParameters,
        SpeciesAndBreed speciesAndBreed,
        HealthInfo healthInfo,
        Address address,
        DateOnly birthDate,
        CurrentStatus currentStatus) : base(id)
    {
        NameAndDescription = nameAndDescription;
        PhysicalParameters = physicalParameters;
        SpeciesAndBreed = speciesAndBreed;
        HealthInfo = healthInfo;
        Address = address;
        BirthDate = birthDate;
        CurrentStatus = currentStatus;
        CreatedAt = DateTime.Now;
    }
    public NameAndDescription NameAndDescription { get; private set; } = null!;
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
    public static Pet Create(
        PetId id,
        NameAndDescription nameAndDescription,
        PhysicalParameters physicalParameters,
        SpeciesAndBreed speciesAndBreed,
        HealthInfo healthInfo,
        Address address,
        DateOnly birthDate,
        CurrentStatus currentStatus)
    {
        return new(
            id,
            nameAndDescription,
            physicalParameters,
            speciesAndBreed,
            healthInfo,
            address,
            birthDate,
            currentStatus);
    }
}