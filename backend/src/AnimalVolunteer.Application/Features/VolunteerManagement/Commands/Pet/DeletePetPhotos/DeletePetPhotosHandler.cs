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
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.DeletePetPhotos;

public class DeletePetPhotosHandler : ICommandHandler<DeletePetPhotosCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    private readonly IValidator<DeletePetPhotosCommand> _validator;

    public DeletePetPhotosHandler(
        IReadDbContext readDbContext,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeletePetPhotosHandler> logger,
        IValidator<DeletePetPhotosCommand> validator)
    {
        _readDbContext = readDbContext;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeletePetPhotosCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var petId = PetId.CreateWithGuid(command.PetId);

        var petExists = await _readDbContext.Pets
            .AnyAsync(p => p.Id == (Guid)petId, cancellationToken);
        if (petExists == false)
            return Errors.General.NotFound(petId).ToErrorList();

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteerResult = await _volunteerRepository
            .GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var deleteResult = volunteer.DeletePetPhotos(petId);
        if (deleteResult.IsFailure)
            return deleteResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Successfully deleted all pet photos of pet with id {petId}", 
            petId);

        return Result.Success<ErrorList>();
    }
}
