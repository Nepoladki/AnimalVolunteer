using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid Id,
    FullNameDto FullName,
    string Email,
    string Description);
