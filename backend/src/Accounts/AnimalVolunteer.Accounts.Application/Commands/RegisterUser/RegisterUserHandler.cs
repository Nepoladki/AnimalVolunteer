using AnimalVolunteer.Accounts.Application.Extensions;
using AnimalVolunteer.Accounts.Domain.Models.Users;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    public RegisterUserHandler(
        UserManager<User> userManager, 
        ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User { UserName = command.UserName, Email = command.Email };

        var creationResult = await _userManager.CreateAsync(user, command.Password);
        if (creationResult.Succeeded == false)
            return creationResult.Errors.ToDomainErrors();

        _logger.LogInformation("Created user with username {Name}", command.UserName);

        return UnitResult.Success<ErrorList>();
    }
}
