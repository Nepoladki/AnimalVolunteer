using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Volunteer;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.CreateVolunteer;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.Description).NotEmpty();

        RuleFor(c => new { c.FirstName, c.SurName, c.LastName })
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.SurName, x.LastName));
        
        RuleForEach(c => c.ContactInfoList).ChildRules(contacts =>
        {
            contacts.RuleFor(x => new { x.PhoneNumber, x.Name, x.Note })
                .MustBeValueObject(z => ContactInfo.Create(z.PhoneNumber, z.Name, z.Note));
        });

        RuleForEach(c => c.SocialNetworkList).ChildRules(networks =>
        {
            networks.RuleFor(x => new { x.Name, x.URL })
                .MustBeValueObject(z => SocialNetwork.Create(z.Name, z.URL));
        });

        RuleForEach(c => c.PaymentDetailsList).ChildRules(payments =>
        {
            payments.RuleFor(x => new { x.Name, x.Descrtiption })
                .MustBeValueObject(z => PaymentDetails.Create(z.Name, z.Descrtiption));
        });
    }
}
