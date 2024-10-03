using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.ContactInfo;

public record UpdateVolunteerContactInfoCommand(
    Guid Id,
    IEnumerable<ContactInfoDto> ContactInfos) : ICommand;
