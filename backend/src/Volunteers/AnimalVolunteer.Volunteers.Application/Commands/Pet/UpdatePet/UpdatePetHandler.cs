using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;
using AnimalVolunteer.Core;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Species.Contracts;
using AnimalVolunteer.Species.Contracts.Requests;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePet;

public class UpdatePetHandler : ICommandHandler<UpdatePetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesContract _speciesContract;
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly ILogger<UpdatePetHandler> _logger;

    public UpdatePetHandler(
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<UpdatePetCommand> validator,
        ILogger<UpdatePetHandler> logger,
        ISpeciesContract speciesContract)
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
        _speciesContract = speciesContract;
    }


    public async Task<UnitResult<ErrorList>> Handle(
        UpdatePetCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var breedAndSpeciesExistResult = await _speciesContract.SpeciesAndBreedExists(
            new SpeciesAndBreedExistRequest(command.SpeciesId, command.BreedId), 
            cancellationToken);
        if (breedAndSpeciesExistResult.IsFailure)
            return breedAndSpeciesExistResult.Error.ToErrorList();

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteerResult = await _volunteerRepository
            .GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var petId = PetId.CreateWithGuid(command.PetId);

        var name = Name.Create(command.Name).Value;

        var description = Description.Create(
            command.Description).Value;

        var physicalParamaters = PhysicalParameters.Create(
            command.Color,
            command.Weight,
            command.Height).Value;

        var speciesAndBreed = SpeciesAndBreed
            .Create(command.SpeciesId, command.BreedId).Value;

        var healthInfo = HealthInfo.Create(
            command.HealthDescription,
            command.IsVaccinated,
            command.IsNeutered).Value;

        var address = Address.Create(
            command.Country,
            command.City,
            command.Street,
            command.House).Value;

        var contactInfos = new ValueObjectList<ContactInfo>(
            command.ContactInfo.Select(x => ContactInfo
                .Create(x.PhoneNumber, x.Name, x.Note).Value));

        var paymentDetails = new ValueObjectList<PaymentDetails>(
            command.PaymentDetails.Select(x => PaymentDetails
                .Create(x.Name, x.Description).Value));

        var updateResult = volunteer.UpdatePet(
            petId,
            name,
            description,
            physicalParamaters,
            speciesAndBreed,
            healthInfo,
            address,
            command.BirthDate,
            command.CurrentStatus,
            contactInfos,
            paymentDetails);
        if (updateResult.IsFailure)
            return updateResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Succsessfully updated pet with id {petId}", petId);

        return Result.Success<ErrorList>();
    }
}
