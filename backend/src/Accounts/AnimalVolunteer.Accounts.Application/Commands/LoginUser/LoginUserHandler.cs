﻿using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Accounts.Application.Commands.LoginUser;

public class LoginUserHandler : ICommandHandler<string, LoginUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(
        UserManager<User> userManager,
        IJwtTokenProvider jwtTokenProvider,
        ILogger<LoginUserHandler> logger)
    {
        _userManager = userManager;
        _jwtTokenProvider = jwtTokenProvider;
        _logger = logger;
    }

    public async Task<Result<string, ErrorList>> Handle( 
        LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
            return Errors.Authentication.WrongCredentials().ToErrorList();

        var passwordIsCorrect = await _userManager
            .CheckPasswordAsync(user, command.Password);
        if (passwordIsCorrect == false)
            return Errors.Authentication.WrongCredentials().ToErrorList();

        var token = _jwtTokenProvider.GenerateAccessToken(user, cancellationToken);

        _logger.LogInformation("User with email {email} signed in", command.Email);

        return token;
    }

}
