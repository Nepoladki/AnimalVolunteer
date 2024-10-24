﻿using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.SocialNetworks;

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
