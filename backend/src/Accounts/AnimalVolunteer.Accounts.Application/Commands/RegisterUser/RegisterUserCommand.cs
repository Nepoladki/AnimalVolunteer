using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand;
