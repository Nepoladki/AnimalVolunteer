﻿using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Volunteers;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.PaymentDetails;

public record UpdateVolunteerPaymentDetailsCommand(
    Guid Id,
    IEnumerable<PaymentDetailsDto> PaymentDetails) : ICommand;
