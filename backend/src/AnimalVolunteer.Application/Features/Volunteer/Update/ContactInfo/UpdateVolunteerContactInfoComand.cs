using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public record UpdateVolunteerContactInfoComand(
    Guid Id,
    ContactInfoListDto ContactInfoList);
