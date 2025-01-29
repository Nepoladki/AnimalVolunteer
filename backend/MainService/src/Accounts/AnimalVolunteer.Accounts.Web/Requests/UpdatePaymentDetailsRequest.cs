using AnimalVolunteer.Accounts.Application.Commands.UpdatePaymentDetails;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Accounts.Web.Requests;

public record UpdatePaymentDetailsRequest(List<PaymentDetailsDto> PaymentDetailsDtos)
{
    public UpdatePaymentDetailsCommand ToCommand(Guid userId) =>
        new(userId, PaymentDetailsDtos);
}

