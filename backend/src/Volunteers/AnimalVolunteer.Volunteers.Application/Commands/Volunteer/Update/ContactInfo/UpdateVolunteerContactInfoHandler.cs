using DomainContactInfo = AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using AnimalVolunteer.Core;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Volunteers.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.ContactInfo;

public class UpdateVolunteerContactInfoHandler
    : ICommandHandler<Guid, UpdateVolunteerContactInfoCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerContactInfoHandler> _logger;
    private readonly IValidator<UpdateVolunteerContactInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateVolunteerContactInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerContactInfoHandler> logger,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerContactInfoCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateVolunteerContactInfoCommand command,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository
            .GetById(command.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var contactInfo = command.ContactInfos.Select(x =>
                DomainContactInfo.Create(x.PhoneNumber, x.Name, x.Note).Value).ToList();

        volunteerResult.Value.UpdateContactInfo(contactInfo);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
}
