﻿using AnimalVolunteer.Accounts.Application.Commands.RegisterUser;
using AnimalVolunteer.Core.DTOs.Common;
using System.Reflection.Metadata.Ecma335;

namespace AnimalVolunteer.Accounts.Web.Requests;

public record RegisterUserRequest(
    string Email, 
    FullNameDto FullName, 
    string UserName, 
    string Password)
{
    public RegisterUserCommand ToCommand() => 
        new(Email, FullName, UserName, Password);
};
