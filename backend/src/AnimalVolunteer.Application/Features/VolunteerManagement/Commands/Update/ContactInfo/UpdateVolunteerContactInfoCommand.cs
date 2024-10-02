using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.ContactInfo;

public record UpdateVolunteerContactInfoCommand(
    Guid Id,
    IEnumerable<ContactInfoDto> ContactInfos);
