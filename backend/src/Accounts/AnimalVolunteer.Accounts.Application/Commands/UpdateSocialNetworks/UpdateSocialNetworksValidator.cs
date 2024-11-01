using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworksValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialNetworksValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
            .WithError(Errors.General.InvalidValue("UserId"));

        RuleForEach(x => x.SocialNetworks)
            .MustBeValueObject(sn => SocialNetwork.Create(sn.Name, sn.URL));
    }
}

