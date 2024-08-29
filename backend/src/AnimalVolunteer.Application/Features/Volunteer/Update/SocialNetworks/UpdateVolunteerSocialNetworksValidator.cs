using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;

public class UpdateVolunteerSocialNetworksValidator : AbstractValidator<UpdateVolunteerSocialNetworksRequest>
{
    public UpdateVolunteerSocialNetworksValidator()
    {
        RuleForEach(r => r.SocialNetworksList.Value).ChildRules(networks =>
        {
            networks.RuleFor(x => new { x.Name, x.URL })
                .MustBeValueObject(z => SocialNetwork.Create(z.Name, z.URL));
        });
    }
}
