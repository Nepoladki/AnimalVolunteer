﻿using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.SocialNetworks;

public record UpdateVolunteerSocialNetworksCommand(
    Guid Id,
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;
