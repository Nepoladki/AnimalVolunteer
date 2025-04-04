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

        RuleFor(x => x.SocialNetworks).NotNull()
            .WithError(Errors.General.InvalidValue("SocialNetworks"));

        RuleFor(x => x.SocialNetworks.Count()).LessThan(6)
            .WithError(Errors.General.InvalidValue("SocialNetworks"));

        RuleForEach(x => x.SocialNetworks)
            .MustBeValueObject(sn => SocialNetwork.Create(sn.Name, sn.URL));
    }
}

