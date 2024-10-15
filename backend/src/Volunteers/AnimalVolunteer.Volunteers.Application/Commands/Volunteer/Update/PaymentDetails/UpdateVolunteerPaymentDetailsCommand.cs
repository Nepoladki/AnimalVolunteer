using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.PaymentDetails;

public record UpdateVolunteerPaymentDetailsCommand(
    Guid Id,
    IEnumerable<PaymentDetailsDto> PaymentDetails) : ICommand;
