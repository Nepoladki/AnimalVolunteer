using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.SocialNetworks;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer;

public record UpdateVolunteerSocialNetworksRequest(
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateVolunteerSocialNetworksCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, SocialNetworks);
}
