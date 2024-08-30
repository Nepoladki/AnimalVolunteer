using AnimalVolunteer.Application.Validation;
using DomainContactInfo = AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerContactInfoValidator : AbstractValidator<UpdateVolunteerContactInfoRequest>
{
    public UpdateVolunteerContactInfoValidator()
    {
        RuleForEach(r => r.ContactInfoList.Value).ChildRules(contactInfo =>
        {
            contactInfo.RuleFor(x => new { x.PhoneNumber, x.Name, x.Note })
                .MustBeValueObject(z => DomainContactInfo.Create(z.PhoneNumber, z.Name, z.Note));
        });
    }
}
