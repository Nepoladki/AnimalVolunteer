using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.ContactInfo;

public record UpdateVolunteerContactInfoCommand(
    Guid Id,
    IEnumerable<ContactInfoDto> ContactInfos) : ICommand;
