using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.MainInfo;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Email,
    string Description)
{
    public UpdateVolunteerMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, FullName, Email, Description);
}