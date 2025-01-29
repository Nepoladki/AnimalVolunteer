using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Domain.Models;

public sealed class Role : IdentityRole<Guid>
{
    public List<User> Users { get; set; } = [];
    public List<RolePermission> RolePermissions { get; set; } = [];
}
