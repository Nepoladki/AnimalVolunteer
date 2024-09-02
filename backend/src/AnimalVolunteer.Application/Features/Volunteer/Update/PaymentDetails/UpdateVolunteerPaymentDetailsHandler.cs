using DomainPaymentDetails = AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetails;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerPaymentDetailsHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerPaymentDetailsHandler> _logger;
    public UpdateVolunteerPaymentDetailsHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateVolunteerPaymentDetailsHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Update(
        UpdateVolunteerPaymentDetailsRequest request, 
        CancellationToken cancellationToken)
    {
        var volunteer = await _volunteerRepository.GetById(request.Id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(request.Id);

        var paymentDetails = PaymentDetailsList.Create(
            request.PaymentDetailsList.Value.Select(x => 
                DomainPaymentDetails.Create(x.Name, x.Description).Value));

        volunteer.UpdatePaymentDetails(paymentDetails);

        await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteer.Id);

        return (Guid)volunteer.Id;
    }
}
