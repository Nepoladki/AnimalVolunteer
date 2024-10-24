using AnimalVolunteer.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure;

public class RolePermissonManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public RolePermissonManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task AddRolesPermissionsIfNotExists(Guid roleId, IEnumerable<string> permissionCodeNames)
    {
        foreach (var permissionCodeName in permissionCodeNames)
        {
            var permission = await _accountsDbContext.Permissions
                .FirstOrDefaultAsync(p => p.CodeName == permissionCodeName);

            var rolePermissionExists = await _accountsDbContext.RolesPermissions
                    .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission!.Id);
            if (rolePermissionExists)
                return;

            await _accountsDbContext.RolesPermissions
                .AddAsync(new RolePermission 
                {
                    RoleId = roleId,
                    PermissionId = permission!.Id
                });
        }

        await _accountsDbContext.SaveChangesAsync();
    }
}
