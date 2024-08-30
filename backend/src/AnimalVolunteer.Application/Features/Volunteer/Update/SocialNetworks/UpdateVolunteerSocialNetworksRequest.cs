using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;

public record UpdateVolunteerSocialNetworksRequest(
    Guid Id,
    SocialNetworksListDto SocialNetworksList);
