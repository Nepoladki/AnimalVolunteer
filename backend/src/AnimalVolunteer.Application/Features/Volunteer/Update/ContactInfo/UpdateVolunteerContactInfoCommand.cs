using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public record UpdateVolunteerContactInfoCommand(
    Guid Id,
    ContactInfoListDto ContactInfoList);
