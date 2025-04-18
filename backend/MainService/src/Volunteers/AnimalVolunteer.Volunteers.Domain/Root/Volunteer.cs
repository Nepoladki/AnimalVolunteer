﻿using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using AnimalVolunteer.Volunteers.Domain.Enums;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;
using AnimalVolunteer.Volunteers.Domain.Entities;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.BaseClasses;

namespace AnimalVolunteer.Volunteers.Domain.Root;

public class Volunteer : SoftDeletableEntity<VolunteerId>
{
    // EF Core ctor
    private Volunteer(VolunteerId id) : base(id) { }
    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        Statistics statistics,
        ValueObjectList<ContactInfo> contactInfoList) : base(id)
    {
        _pets = [];
        FullName = fullName;
        Email = email;
        Description = description;
        Statistics = statistics;
        ContactInfoList = contactInfoList;
    }

    private readonly List<Pet> _pets = default!;
    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public Statistics Statistics { get; private set; } = null!;
    public IReadOnlyList<ContactInfo> ContactInfoList { get; private set; } = null!;
    public IReadOnlyList<Pet> Pets => _pets;
    public int CountPetsFoundedHome() => Statistics.PetsFoundedHome;
    public int CountPetsLookingForHome() => Statistics.PetsLookingForHome;
    public int CountPetsInVetClinic() => Statistics.PetsInVetClinic;

    public static Volunteer Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        Statistics statistics)
    {
        var contactInfoList = new List<ContactInfo>();

        return new(
        id,
        fullName,
        email,
        description,
        statistics,
        contactInfoList);
    }
    public void UpdateMainInfo(FullName fullName, Email email, Description description)
    {
        FullName = fullName;
        Email = email;
        Description = description;
    }

    public void UpdateContactInfo(ValueObjectList<ContactInfo> contacts) =>
        ContactInfoList = contacts;

    public override void SoftDelete()
    {
        base.SoftDelete();

        foreach (var pet in _pets)
        {
            pet.SoftDelete();
        }
    }

    public override void Restore()
    {
        base.Restore();

        foreach (var pet in _pets)
        {
            pet.Restore();
        }
    }

    public UnitResult<Error> SoftDeletePet(PetId petId)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        petResult.Value.SoftDelete();

        return UnitResult.Success<Error>();
    }

    public Result<string[], Error> HardDeletePet(PetId petId)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        var filesToDelete = petResult.Value.PetPhotosList
            .Select(p => p.FilePath.Value).ToArray();

        _pets.Remove(petResult.Value);

        return filesToDelete;
    }

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);

        if (pet is null)
            return Errors.Volunteer.PetNotFound(Id, petId);

        return pet;
    }

    public UnitResult<Error> ChangePetStatus(PetId petId, CurrentStatus status)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        if (status == CurrentStatus.HomeFounded)
            return Errors.Volunteer.PetStatusRestriction(Id);

        petResult.Value.ChangeStatus(status);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var position = Position.Create(_pets.Count + 1);
        if (position.IsFailure)
            return position.Error;

        pet.MoveToPosition(position.Value);

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

    public UnitResult<Error> UpdatePet(
        PetId petId,
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
        var pet = Pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.Volunteer.PetNotFound(Id, petId);

        pet.UpdatePet(
            name,
            description,
            physicalParameters,
            speciesAndBreed,
            healthInfo,
            address,
            birthDate,
            currentStatus,
            contactInfos,
            paymentDetails);

        return Result.Success<Error>();
    }

    public UnitResult<Error> UpdatePetPhotos(PetId petId, List<PetPhoto> petPhotos)
    {
        var pet = Pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.Volunteer.PetNotFound(Id, petId);

        pet.UpdatePhotos(petPhotos);

        return Result.Success<Error>();
    }

    public UnitResult<Error> DeletePetPhotos(PetId petId)
    {
        var pet = Pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.Volunteer.PetNotFound(Id, petId);

        pet.DeleteAllPhotos();

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
        else
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