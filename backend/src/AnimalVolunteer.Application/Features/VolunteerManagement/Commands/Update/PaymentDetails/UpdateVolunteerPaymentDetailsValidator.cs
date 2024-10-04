using AnimalVolunteer.Application.Validation;
using DomainPaymentDetails = AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetails;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.PaymentDetails;

public class UpdateVolunteerPaymentDetailsValidator : AbstractValidator<UpdateVolunteerPaymentDetailsCommand>
{
    public UpdateVolunteerPaymentDetailsValidator()
    {
        RuleForEach(r => r.PaymentDetails).ChildRules(paymentDetails =>
        {
            paymentDetails.RuleFor(x => new { x.Name, x.Description })
                .MustBeValueObject(z => DomainPaymentDetails.Create(z.Name, z.Description));
        });
    }
}
