using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Accounts.Application.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid UserId, List<SocialNetworkDto> SocialNetworks) : ICommand;

