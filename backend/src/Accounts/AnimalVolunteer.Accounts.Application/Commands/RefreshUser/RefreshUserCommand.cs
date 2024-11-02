using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Accounts.Application.Commands.RefreshUser;

public record RefreshUserCommand(string AccessToken, Guid RefreshToken) : ICommand;
