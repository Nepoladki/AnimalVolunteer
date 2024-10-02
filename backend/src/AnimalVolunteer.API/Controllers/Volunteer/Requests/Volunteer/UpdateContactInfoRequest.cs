using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.ContactInfo;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record UpdateVolunteerContactInfoRequest(
    IEnumerable<ContactInfoDto> ContactInfos)
{
    public UpdateVolunteerContactInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, ContactInfos);
}
