using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.PaymentDetails;

public record UpdateVolunteerPaymentDetailsCommand(
    Guid Id,
    IEnumerable<PaymentDetailsDto> PaymentDetails) : ICommand;
