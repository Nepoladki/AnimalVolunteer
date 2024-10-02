﻿using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.SocialNetworks;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record UpdateVolunteerSocialNetworksRequest(
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateVolunteerSocialNetworksCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, SocialNetworks);
}