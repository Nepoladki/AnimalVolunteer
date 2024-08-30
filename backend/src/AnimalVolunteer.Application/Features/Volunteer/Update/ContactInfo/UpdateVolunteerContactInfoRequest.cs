using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public record UpdateVolunteerContactInfoRequest(
    Guid Id,
    ContactInfoListDto ContactInfoList);
