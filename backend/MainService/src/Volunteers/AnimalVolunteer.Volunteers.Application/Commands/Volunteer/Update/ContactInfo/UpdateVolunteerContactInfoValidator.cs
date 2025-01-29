using DomainContactInfo = AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo;
using FluentValidation;
using AnimalVolunteer.Core.Validation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.ContactInfo;

public class UpdateVolunteerContactInfoValidator :
    AbstractValidator<UpdateVolunteerContactInfoCommand>
{
    public UpdateVolunteerContactInfoValidator()
    {
        RuleForEach(r => r.ContactInfos).ChildRules(contactInfo =>
        {
            contactInfo.RuleFor(x => new {x.PhoneNumber, x.Name, x.Note})
                .MustBeValueObject(z => DomainContactInfo.Create(
                    z.PhoneNumber, z.Name, z.Note));
        });
    }
}
