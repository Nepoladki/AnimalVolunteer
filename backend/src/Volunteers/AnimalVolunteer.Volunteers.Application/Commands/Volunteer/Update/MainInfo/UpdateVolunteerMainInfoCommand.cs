using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Volunteers;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.MainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid Id,
    FullNameDto FullName,
    string Email,
    string Description) : ICommand;
