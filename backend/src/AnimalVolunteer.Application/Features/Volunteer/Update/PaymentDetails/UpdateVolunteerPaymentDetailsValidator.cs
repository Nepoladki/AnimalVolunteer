using AnimalVolunteer.Application.Validation;
using DomainPaymentDetails = AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetails;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerPaymentDetailsValidator : AbstractValidator<UpdateVolunteerPaymentDetailsCommand>
{
    public UpdateVolunteerPaymentDetailsValidator()
    {
        RuleForEach(r => r.PaymentDetailsList.Value).ChildRules(paymentDetails =>
        {
            paymentDetails.RuleFor(x => new { x.Name, x.Description })
                .MustBeValueObject(z => DomainPaymentDetails.Create(z.Name, z.Description));
        });
    }
}
