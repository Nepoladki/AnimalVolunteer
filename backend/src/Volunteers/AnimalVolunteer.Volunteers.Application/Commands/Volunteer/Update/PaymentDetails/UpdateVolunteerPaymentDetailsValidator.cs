using DomainEntities = AnimalVolunteer.SharedKernel.ValueObjects;
using FluentValidation;
using AnimalVolunteer.Core.Validation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerPaymentDetailsValidator 
    : AbstractValidator<UpdateVolunteerPaymentDetailsCommand>
{
    public UpdateVolunteerPaymentDetailsValidator()
    {
        RuleForEach(r => r.PaymentDetails).ChildRules(paymentDetails =>
        {
            paymentDetails.RuleFor(x => new { x.Name, x.Description })
                .MustBeValueObject(z => DomainEntities.PaymentDetails.Create(
                    z.Name, 
                    z.Description));
        });
    }
}
