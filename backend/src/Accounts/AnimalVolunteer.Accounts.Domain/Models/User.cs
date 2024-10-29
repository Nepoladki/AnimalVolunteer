﻿using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Domain.Models;

public sealed class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = default!;
    public string Photo { get; set; } = string.Empty;
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public static User CreateAdmin(string userName, string email, Role role)
    {
        return new User
        {
            Photo = "",
            FullName = "admin",
            SocialNetworks = [],
            UserName = userName,
            Email = email,
            Role = role
        };
    }
}