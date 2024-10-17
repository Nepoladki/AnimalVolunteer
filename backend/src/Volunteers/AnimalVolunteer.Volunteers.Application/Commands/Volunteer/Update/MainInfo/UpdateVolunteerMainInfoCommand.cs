using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.MainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid Id,
    FullNameDto FullName,
    string Email,
    string Description) : ICommand;
