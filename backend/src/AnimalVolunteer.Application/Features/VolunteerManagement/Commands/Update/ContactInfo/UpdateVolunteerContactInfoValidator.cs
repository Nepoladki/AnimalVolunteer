using AnimalVolunteer.Application.Validation;
using DomainContactInfo = AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.ContactInfo;

public class UpdateVolunteerContactInfoValidator :
    AbstractValidator<UpdateVolunteerContactInfoCommand>
{
    public UpdateVolunteerContactInfoValidator()
    {
        RuleForEach(r => r.ContactInfos).ChildRules(contactInfo =>
        {
            contactInfo.RuleFor(x => new { x.PhoneNumber, x.Name, x.Note })
                .MustBeValueObject(z => DomainContactInfo.Create(
                    z.PhoneNumber, z.Name, z.Note));
        });
    }
}
