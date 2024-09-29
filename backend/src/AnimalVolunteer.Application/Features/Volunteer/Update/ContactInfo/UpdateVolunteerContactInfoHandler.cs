using DomainContactInfo = AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo;
using AnimalVolunteer.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Application.Database;
using FluentValidation;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerContactInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerContactInfoHandler> _logger;
    private readonly IValidator<UpdateVolunteerContactInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateVolunteerContactInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerContactInfoHandler> logger,
        IUnitOfWork unitOfWork,
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

        var contactInfo = ContactInfoList.Create(
            command.ContactInfoList.Value.Select(x =>
                DomainContactInfo.Create(x.PhoneNumber, x.Name, x.Note).Value));

        volunteerResult.Value.UpdateContactInfo(contactInfo);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
}
