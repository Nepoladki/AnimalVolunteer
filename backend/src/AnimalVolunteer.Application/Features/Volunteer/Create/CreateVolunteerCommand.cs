﻿using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    IEnumerable<SocialNetworkDto> SocialNetworkList,
    IEnumerable<PaymentDetailsDto> PaymentDetailsList);
