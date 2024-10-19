using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Volunteers;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.ContactInfo;

public record UpdateVolunteerContactInfoCommand(
    Guid Id,
    IEnumerable<ContactInfoDto> ContactInfos) : ICommand;
