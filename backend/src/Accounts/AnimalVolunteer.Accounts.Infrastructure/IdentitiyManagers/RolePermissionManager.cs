﻿using AnimalVolunteer.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class RolePermissionManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public RolePermissionManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task AddRolesPermissionsIfNotExists(Guid roleId, IEnumerable<string> permissionCodeNames)
    {
        foreach (var permissionCodeName in permissionCodeNames)
        {
            var permission = await _accountsDbContext.Permissions
                .FirstOrDefaultAsync(p => p.CodeName == permissionCodeName);
            if (permission is null)
                throw new ApplicationException($"Permission with codename {permissionCodeName} was not found in database");

            var rolePermissionExists = await _accountsDbContext.RolesPermissions
                    .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission!.Id);
            if (rolePermissionExists)
                continue;

            _accountsDbContext.RolesPermissions
                .Add(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permission!.Id
                });
        }

        await _accountsDbContext.SaveChangesAsync();
    }
}
