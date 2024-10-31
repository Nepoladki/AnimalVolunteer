using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.SocialNetworks;

public record UpdateVolunteerSocialNetworksCommand(
    Guid Id,
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;
