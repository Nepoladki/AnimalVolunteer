using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public record RegisterUserCommand(
    string Email, 
    string FirstName,
    string LastName,
    string? Patronymic,
    string UserName, 
    string Password) : ICommand;
