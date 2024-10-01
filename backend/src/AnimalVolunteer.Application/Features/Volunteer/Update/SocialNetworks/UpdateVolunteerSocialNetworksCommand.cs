using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;

public record UpdateVolunteerSocialNetworksCommand(
    Guid Id,
    IEnumerable<SocialNetworkDto> SocialNetworks);
