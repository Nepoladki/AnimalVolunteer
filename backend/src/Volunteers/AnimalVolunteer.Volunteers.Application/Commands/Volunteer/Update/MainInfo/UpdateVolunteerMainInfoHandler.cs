using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using AnimalVolunteer.Core;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Volunteers.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.MainInfo;

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
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork)
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
            command.FullName.Patronymic,
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
