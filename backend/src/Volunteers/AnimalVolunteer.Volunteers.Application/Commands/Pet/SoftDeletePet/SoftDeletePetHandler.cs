using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.SoftDeletePet;

public class SoftDeletePetHandler : ICommandHandler<SoftDeletePetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeletePetCommand> _validator;
    private readonly ILogger<SoftDeletePetHandler> _logger;

    public SoftDeletePetHandler(
        ILogger<SoftDeletePetHandler> logger,
        IValidator<SoftDeletePetCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IVolunteerRepository volunteerRepository)
    {
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        SoftDeletePetCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteerResult = await _volunteerRepository
            .GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.CreateWithGuid(command.PetId);

        volunteerResult.Value.SoftDeletePet(petId);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Softdeleted pet (id = {petId}) belonging to volunteer (id = {vid})",
            petId,
            volunteerId);

        return UnitResult.Success<ErrorList>();
    }
}
