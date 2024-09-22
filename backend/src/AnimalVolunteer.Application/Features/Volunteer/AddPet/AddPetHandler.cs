using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPet;

public class AddPetHandler
{
    private const string BUCKET_NAME = "photos";
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Add(
        AddPetCommand command, CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        var volunteer = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(command.VolunteerId);
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

            List<PetPhoto> domainPhotoFiles = [];
            List<PetPhotoDto> photosToUpload = [];

            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.Filename);

                var filePathResult = FilePath.Create(Guid.NewGuid(), extension);

                if (filePathResult.IsFailure)
                    return filePathResult.Error;

                var photoToUpload = new PetPhotoDto(
                    filePathResult.Value.Path, file.Content, file.IsMain);

                photosToUpload.Add(photoToUpload);

                var domainPetPhoto = PetPhoto.Create(filePathResult.Value, file.IsMain);

                domainPhotoFiles.Add(domainPetPhoto.Value);
            }

            var domainPhotoList = PetPhotoList.Create(domainPhotoFiles);

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
                domainPhotoList);

            volunteer.AddPet(pet);

            var uploadResult = await _fileProvider
                    .UploadFiles(
                    photosToUpload,
                    BUCKET_NAME,
                    cancellationToken);

            if (uploadResult.IsFailure)
                return uploadResult.Error;

            await _unitOfWork.SaveChanges(cancellationToken);

            transaction.Commit();

            return (Guid)volunteer.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cannot add pet to volunteer with id: {id}", command.VolunteerId);

            transaction.Rollback();

            return Error.Failure("volunteer.pet.failure", "Cannot add pet to volunteer with id: {id}");
        }
    }
}
