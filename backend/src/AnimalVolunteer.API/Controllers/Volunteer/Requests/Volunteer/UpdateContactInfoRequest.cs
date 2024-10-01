using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record UpdateVolunteerContactInfoRequest(
    IEnumerable<ContactInfoDto> ContactInfos)
{
    public UpdateVolunteerContactInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, ContactInfos);
}
