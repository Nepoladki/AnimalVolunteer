using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Domain.Models.Users;

public sealed class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}