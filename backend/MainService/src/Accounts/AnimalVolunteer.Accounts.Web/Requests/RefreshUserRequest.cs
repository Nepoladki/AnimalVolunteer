using AnimalVolunteer.Accounts.Application.Commands.RefreshUser;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace AnimalVolunteer.Accounts.Web.Requests;

public record RefreshUserRequest(string AccessToken, string RefreshToken)
{
    public RefreshUserCommand ToCommand() =>
        new(AccessToken, Guid.Parse(RefreshToken));
};

