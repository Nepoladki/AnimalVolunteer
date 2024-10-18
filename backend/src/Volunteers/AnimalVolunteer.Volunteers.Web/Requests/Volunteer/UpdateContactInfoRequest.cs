using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.ContactInfo;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer;

public record UpdateVolunteerContactInfoRequest(
    IEnumerable<ContactInfoDto> ContactInfos)
{
    public UpdateVolunteerContactInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, ContactInfos);
}
