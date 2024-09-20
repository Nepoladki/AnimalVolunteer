using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPet;

public class AddPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public AddPetHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Add(
        AddPetCommand command, CancellationToken cancellationToken = default)
    {
        var volunteer = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(command.VolunteerId);

        var petId = PetId.Create();

        var name = Name.Create(command.Name).Value;

        var description = Description
            .Create(command.Description).Value;

        var physicalParamaters = PhysicalParameters
            .Create(
            command.Color, 
            command.Weight, 
            command.Height).Value;

        var speciesAndBreed = SpeciesAndBreed
            .Create(command.SpeciesId, command.BreedId).Value;

        var healthInfo = HealthInfo
            .Create(
            command.HealthDescription, 
            command.IsVaccinated, 
            command.IsNeutered).Value;
        
        var address = Address
            .Create(
            command.Country, 
            command.City, 
            command.Street, 
            command.House).Value;

        var status = (CurrentStatus)Enum
            .Parse(typeof(CurrentStatus), command.CurrentStatus);

        var petPhotoList = command.Files
            .Select(f => PetPhoto.Create(f.Filename, f.IsMain).Value);

        foreach (var file in command.Files)
        {
            var filePath = Guid.NewGuid() + "jpg";

            var fileData = 
        }

        var pet = new Pet(
            petId,
            name, 
            description,
            physicalParamaters,
            speciesAndBreed,
            healthInfo,
            address,
            command.BirthDate,
            status);

        volunteer.AddPet(pet);

        await _volunteerRepository.Save(volunteer, cancellationToken);

        return (Guid)volunteer.Id;
    }
}
