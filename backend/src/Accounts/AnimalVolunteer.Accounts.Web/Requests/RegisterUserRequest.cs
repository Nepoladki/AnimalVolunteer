using AnimalVolunteer.Accounts.Application.Commands.RegisterUser;
using System.Reflection.Metadata.Ecma335;

namespace AnimalVolunteer.Accounts.Web.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand() => new(Email, UserName, Password);
};
