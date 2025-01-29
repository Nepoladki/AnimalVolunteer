using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Accounts.Application.Commands.UpdatePaymentDetails;

public record UpdatePaymentDetailsCommand(Guid UserId, List<PaymentDetailsDto> PaymentDetails) : ICommand;

