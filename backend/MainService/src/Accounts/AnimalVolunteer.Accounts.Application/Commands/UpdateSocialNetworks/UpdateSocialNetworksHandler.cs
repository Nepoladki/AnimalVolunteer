using AnimalVolunteer.Accounts.Application.Extensions;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Application.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworksHandler : ICommandHandler<UpdateSocialNetworksCommand>
{
    private readonly IValidator<UpdateSocialNetworksCommand> _validator;
    private readonly UserManager<User> _userManager;

    public UpdateSocialNetworksHandler(
        UserManager<User> userManager, IValidator<UpdateSocialNetworksCommand> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        UpdateSocialNetworksCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
            return Errors.Accounts.UserNotFoud(command.UserId).ToErrorList();

        var newSocials = command.SocialNetworks
            .Select(c => SocialNetwork.Create(c.Name, c.URL).Value).ToList();

        user.SocialNetworks = newSocials;

        var updateResult = await _userManager.UpdateAsync(user);
        if (updateResult.Succeeded == false)
            return updateResult.Errors.ToDomainErrors();

        return UnitResult.Success<ErrorList>();

    }
}

