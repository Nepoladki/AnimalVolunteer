using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Domain.Models;

public sealed class User : IdentityUser<Guid>
{
    private User() { }
    public FullName FullName { get; set; } = default!;
    public string Photo { get; set; } = string.Empty;
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
    public List<Role> Roles { get; set; } = null!;
    
    public static User CreateAdmin(
        string userName, string email, Role role)
    {
        return new User
        {
            Photo = "",
            FullName = FullName.Create("admin", null, "admin").Value,
            SocialNetworks = [],
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }

    public static User CreateParticipant(
        FullName fullName, string userName, string email, Role role)
    {
        return new User
        {
            Photo = "",
            FullName = fullName,
            SocialNetworks = [],
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
}