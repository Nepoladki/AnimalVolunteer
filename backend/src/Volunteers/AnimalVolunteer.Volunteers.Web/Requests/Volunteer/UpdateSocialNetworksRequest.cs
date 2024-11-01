using AnimalVolunteer.Core.DTOs.Common;
using AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.SocialNetworks;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer;

public record UpdateVolunteerSocialNetworksRequest(
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateVolunteerSocialNetworksCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, SocialNetworks);
}
