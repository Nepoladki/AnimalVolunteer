using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Commands.UpdatePaymentDetails;

public class UpdatePaymentDetailsValidator : AbstractValidator<UpdatePaymentDetailsCommand>
{
    public UpdatePaymentDetailsValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().WithError(Errors.General.InvalidValue("UserId"));

        RuleForEach(c => c.PaymentDetails)
            .MustBeValueObject(pd => PaymentDetails.Create(pd.Name, pd.Description));
    }
}

