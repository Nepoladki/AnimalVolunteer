using AnimalVolunteer.Accounts.Application.Commands.RegisterUser;
using System.Reflection.Metadata.Ecma335;

namespace AnimalVolunteer.Accounts.Web.Requests;

public record RegisterUserRequest(
    string Email, 
    string FirstName, 
    string LastName, 
    string? Patronymic, 
    string UserName, 
    string Password)
{
    public RegisterUserCommand ToCommand() => 
        new(Email, FirstName, LastName, Patronymic, UserName, Password);
};
