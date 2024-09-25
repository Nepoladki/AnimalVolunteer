using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPet;

public class AddPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider fileProvider,
        ILogger<AddPetHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _fileProvider = fileProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Add(
        AddPetCommand command, CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        try
        {
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

            var contactInfos = ContactInfoList.CreateEmpty();

            var paymentDetails = PaymentDetailsList.CreateEmpty();

            var status = (CurrentStatus)Enum
                .Parse(typeof(CurrentStatus), command.CurrentStatus);

            var photos = PetPhotoList.Create([]);

            var pet = new Pet(
                petId,
                name,
                description,
                physicalParamaters,
                speciesAndBreed,
                healthInfo,
                address,
                contactInfos,
                command.BirthDate,
                status,
                paymentDetails,
                photos);

            volunteerResult.Value.AddPet(pet);

            await _unitOfWork.SaveChanges(cancellationToken);

            return (Guid)volunteerResult.Value.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cannot add pet to volunteer with id: {id}", command.VolunteerId);
            return Error.Failure("volunteer.pet.failure", "Cannot add pet to volunteer with id: {id}");
        }
    }
}
