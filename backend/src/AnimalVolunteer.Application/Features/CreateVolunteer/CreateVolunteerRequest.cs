using AnimalVolunteer.Application.DTOs;

namespace AnimalVolunteer.Application.Features.CreateVolunteer
{
    public record CreateVolunteerRequest(
        string FirstName,
        string SurName,
        string LastName,
        string Description);
}
