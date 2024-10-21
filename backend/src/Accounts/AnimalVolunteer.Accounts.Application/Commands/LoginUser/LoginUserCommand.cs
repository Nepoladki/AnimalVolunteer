using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Accounts.Application.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : ICommand;
