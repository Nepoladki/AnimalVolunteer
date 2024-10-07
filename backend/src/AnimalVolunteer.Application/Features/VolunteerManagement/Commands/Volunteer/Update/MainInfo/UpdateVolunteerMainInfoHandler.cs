using AnimalVolunteer.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Volunteer;
using AnimalVolunteer.Application.Database;
using FluentValidation;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.MainInfo;

public class UpdateVolunteerMainInfoHandler
    : ICommandHandler<Guid, UpdateVolunteerMainInfoCommand>
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
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateVolunteerMainInfoCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();


        var volunteerResult = await _volunteerRepository
            .GetById(command.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var fullName = FullName.Create(
            command.FullName.FirstName,
            command.FullName.SurName,
            command.FullName.LastName).Value;

        var email = Email.Create(command.Email).Value;

        var description = Description.Create(command.Description).Value;

        var alreadyExist = await _volunteerRepository
            .ExistByEmail(email, cancellationToken);
        if (alreadyExist && volunteerResult.Value.Email != email)
            return Errors.Volunteer.AlreadyExist().ToErrorList();

        volunteerResult.Value.UpdateMainInfo(fullName, email, description);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
};
