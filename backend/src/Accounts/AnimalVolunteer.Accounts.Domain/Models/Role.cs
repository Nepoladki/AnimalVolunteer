using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Domain.Models;

public sealed class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; } = [];
}
