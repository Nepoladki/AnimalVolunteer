﻿using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.PaymentDetails;

public record UpdateVolunteerPaymentDetailsCommand(
    Guid Id,
    IEnumerable<PaymentDetailsDto> PaymentDetails);