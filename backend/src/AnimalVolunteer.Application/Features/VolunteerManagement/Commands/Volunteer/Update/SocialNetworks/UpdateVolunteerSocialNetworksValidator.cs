using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Volunteer;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.SocialNetworks;

public class UpdateVolunteerSocialNetworksValidator : AbstractValidator<UpdateVolunteerSocialNetworksCommand>
{
    public UpdateVolunteerSocialNetworksValidator()
    {
        RuleForEach(r => r.SocialNetworks).ChildRules(networks =>
        {
            networks.RuleFor(x => new { x.Name, x.URL })
                .MustBeValueObject(z => SocialNetwork.Create(z.Name, z.URL));
        });
    }
}
