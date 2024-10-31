using AnimalVolunteer.Accounts.Application.Extensions;
using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IParticipantAccountManager _participantAccountManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly IValidator<RegisterUserCommand> _validator;
    public RegisterUserHandler(
        UserManager<User> userManager,
        ILogger<RegisterUserHandler> logger,
        RoleManager<Role> roleManager,
        IValidator<RegisterUserCommand> validator,
        IParticipantAccountManager participantAccountManager)
    {
        _userManager = userManager;
        _logger = logger;
        _roleManager = roleManager;
        _validator = validator;
        _participantAccountManager = participantAccountManager;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var fullName = FullName.Create(
            command.FullName.FirstName, 
            command.FullName.Patronymic, 
            command.FullName.LastName).Value;

        var role = await _roleManager.FindByNameAsync(ParticipantAccount.PARTICIPANT_ACCOUNT_NAME);
        if (role is null)
            return Errors.Accounts.RoleNotFound(ParticipantAccount.PARTICIPANT_ACCOUNT_NAME).ToErrorList();

        var user = User.CreateParticipant(fullName, command.UserName, command.Email, role);

        var creationResult = await _userManager.CreateAsync(user, command.Password);
        if (creationResult.Succeeded == false)
            return creationResult.Errors.ToDomainErrors();

        var participantAccount = ParticipantAccount.Create(user);

        await _participantAccountManager.AddParticipant(participantAccount,  cancellationToken);

        _logger.LogInformation("Created user with username {Name}", command.UserName);

        return UnitResult.Success<ErrorList>();
    }
}
