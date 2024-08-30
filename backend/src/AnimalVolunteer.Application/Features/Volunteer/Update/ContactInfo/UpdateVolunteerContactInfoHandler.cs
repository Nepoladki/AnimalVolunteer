using DomainContactInfo = AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerContactInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerContactInfoHandler> _logger;
    public UpdateVolunteerContactInfoHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateVolunteerContactInfoHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Update(
        UpdateVolunteerContactInfoRequest request, 
        CancellationToken cancellationToken)
    {
        var volunteer = await _volunteerRepository.GetByID(request.Id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(request.Id);

        var paymentDetails = ContactInfoList.Create(
            request.ContactInfoList.Value.Select(x =>
                DomainContactInfo.Create(x.PhoneNumber, x.Name, x.Note).Value));

        volunteer.UpdateContactInfo(paymentDetails);

        await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteer.Id);

        return (Guid)volunteer.Id;
    }
}
