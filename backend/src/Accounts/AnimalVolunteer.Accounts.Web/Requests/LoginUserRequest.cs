using AnimalVolunteer.Accounts.Application.Commands.LoginUser;

namespace AnimalVolunteer.Accounts.Web.Requests;

public record LoginUserRequest(string Email, string Password)
{
    public LoginUserCommand ToCommand() => new(Email, Password);
};