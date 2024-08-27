using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.CreateVolunteer;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleFor(c => c.FullName)
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.SurName, x.LastName));

        RuleForEach(c => c.SocialNetworkList).ChildRules(networks =>
        {
            networks.RuleFor(x => new { x.Name, x.URL })
                .MustBeValueObject(z => SocialNetwork.Create(z.Name, z.URL));
        });

        RuleForEach(c => c.PaymentDetailsList).ChildRules(payments =>
        {
            payments.RuleFor(x => new { x.Name, x.Description })
                .MustBeValueObject(z => PaymentDetails.Create(z.Name, z.Description));
        });
    }
}
