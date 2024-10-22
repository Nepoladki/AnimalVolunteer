using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Domain.Models;

public sealed class Role : IdentityRole<Guid>
{
    public string Name { get; set; } = string.Empty;
    public List<RolePermission> RolePermissions { get; set; } = [];
}
