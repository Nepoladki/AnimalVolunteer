using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.Application.Database;
using FluentValidation;
using AnimalVolunteer.Application.Extensions;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;

public class UpdateVolunteerMainInfoHandler
{
    private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateVolunteerMainInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateVolunteerMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerMainInfoHandler> logger,
        IValidator<UpdateVolunteerMainInfoCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Update(
        UpdateVolunteerMainInfoCommand command, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        { 
            return validationResult.ToErrorList();
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
            return Errors.Volunteer.AlreadyExist().ToErrorList();

        volunteerResult.Value.UpdateMainInfo(fullName, email, description);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
};
