using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public record UpdateVolunteerPaymentDetailsRequest(
    Guid Id,
    PaymentDetailsListDto PaymentDetailsList);
