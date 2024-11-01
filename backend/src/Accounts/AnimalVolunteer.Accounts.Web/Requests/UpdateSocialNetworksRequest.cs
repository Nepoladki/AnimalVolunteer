using AnimalVolunteer.Accounts.Application.Commands.UpdateSocialNetworks;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Accounts.Web.Requests;

public record UpdateSocialNetworksRequest(List<SocialNetworkDto> SocialNetworkDtos)
{
    public UpdateSocialNetworksCommand ToCommand(Guid userId) =>
        new(userId, SocialNetworkDtos);
}
