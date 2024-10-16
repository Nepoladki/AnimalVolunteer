﻿using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.PaymentDetails;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer;

public record UpdateVolunteerPaymentDetailsRequest(
    IEnumerable<PaymentDetailsDto> PaymentDetails)
{
    public UpdateVolunteerPaymentDetailsCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, PaymentDetails);
}