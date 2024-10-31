using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public record RegisterUserCommand(
    string Email, 
    FullNameDto FullName,
    string UserName, 
    string Password) : ICommand;
