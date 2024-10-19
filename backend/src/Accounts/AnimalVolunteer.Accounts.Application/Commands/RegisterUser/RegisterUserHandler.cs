using AnimalVolunteer.Accounts.Application.Extensions;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    public RegisterUserHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User { UserName = command.UserName, Email = command.Email };

        var creationResult = await _userManager.CreateAsync(user, command.Password);
        if (creationResult.Succeeded == false)
        {
            creationResult.Errors.ToDomainErrors();
        }


    }
}
