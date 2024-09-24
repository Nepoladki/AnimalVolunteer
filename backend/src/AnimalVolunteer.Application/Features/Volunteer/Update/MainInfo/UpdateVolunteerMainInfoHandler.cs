﻿using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.Application.Database;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;

public class UpdateVolunteerMainInfoHandler
{
    private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly IValidator<UpdateVolunteerMainInfoCommand> _validator;
    public UpdateVolunteerMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerMainInfoHandler> logger,
        IApplicationDbContext dbContext,
        IValidator<UpdateVolunteerMainInfoCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _dbContext = dbContext;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Update(
        UpdateVolunteerMainInfoCommand command, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        { 
            var validationErrors = validationResult.Errors;

            var errors = from validationError in validationErrors
                         let errorMessage = validationError.ErrorMessage
                         let error = Error.Deserialize(errorMessage)
                         select Error.Validation(error.Code, error.Message, error.InvalidField);

            return new ErrorList(errors);
        }

        var volunteerResult = await _volunteerRepository
            .GetById(command.Id, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var fullName = FullName.Create(
            command.FirstName,
            command.SurName,
            command.LastName).Value;

        var email = Email.Create(command.Email).Value;

        var description = Description.Create(command.Description).Value;

        if (await _volunteerRepository.ExistByEmail(email, cancellationToken) 
            && volunteerResult.Value.Email != email)
            return Errors.Volunteer.AlreadyExist();


        volunteerResult.Value.UpdateMainInfo(fullName, email, description);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
};
