﻿using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.ChangePetStatus;

public class ChangePetStatusHandler : ICommandHandler<ChangePetStatusCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<ChangePetStatusHandler> _logger;
    private readonly IValidator<ChangePetStatusCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePetStatusHandler(
        IValidator<ChangePetStatusCommand> validator,
        ILogger<ChangePetStatusHandler> logger, 
        IUnitOfWork unitOfWork, 
        IVolunteerRepository volunteerRepository)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        ChangePetStatusCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);
        var petId = PetId.CreateWithGuid(command.PetId);

        var volunteerResult = await _volunteerRepository
            .GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var changeResult = volunteerResult.Value
            .ChangePetStatus(petId, command.NewStatus);
        if (changeResult.IsFailure)
            return changeResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Succsessfully changed volunteer's (id = {vId}) pet's (id = {pId}) status to {newStatus}",
            volunteerId,
            petId,
            command.NewStatus);

        return UnitResult.Success<ErrorList>();
    }
}
