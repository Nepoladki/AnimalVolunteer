using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.MainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid Id,
    FullNameDto FullName,
    string Email,
    string Description) : ICommand;
