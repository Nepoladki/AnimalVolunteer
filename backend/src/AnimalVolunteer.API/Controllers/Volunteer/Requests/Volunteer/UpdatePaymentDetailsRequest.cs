using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record UpdateVolunteerPaymentDetailsRequest(
    IEnumerable<PaymentDetailsDto> PaymentDetails)
{
    public UpdateVolunteerPaymentDetailsCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, PaymentDetails);
}
