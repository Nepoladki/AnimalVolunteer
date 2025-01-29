using AnimalVolunteer.Core.DTOs.Common;
using AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Create;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer
{
    public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description)
    {
        public CreateVolunteerCommand ToCommand()
        {
            return new(
                FullName,
                Email,
                Description);
        }
    }
}
