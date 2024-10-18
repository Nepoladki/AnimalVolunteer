using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.PaymentDetails;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer;

public record UpdateVolunteerPaymentDetailsRequest(
    IEnumerable<PaymentDetailsDto> PaymentDetails)
{
    public UpdateVolunteerPaymentDetailsCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, PaymentDetails);
}
