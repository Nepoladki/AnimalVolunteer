using AnimalVolunteer.Core.DTOs.Common;
using AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.MainInfo;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Email,
    string Description)
{
    public UpdateVolunteerMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, FullName, Email, Description);
}