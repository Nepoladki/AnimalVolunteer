using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure.Repositories;

public class RolesPermissionsRepository(AccountsWriteDbContext accountsContext) : IRolesPermissionsRepository
{
    public async Task<Permission?> GetPermissionByCode(string code)
        => await accountsContext.Permissions.FirstOrDefaultAsync(p => p.CodeName == code);

    public async Task<IEnumerable<Permission>?> GetAllPermissions(CancellationToken cancellationToken = default)
        => await accountsContext.Permissions.ToListAsync(cancellationToken);

    public async Task<IEnumerable<string>> GetAllExistingPermissionsCodes(
        CancellationToken cancellationToken = default)
        => await accountsContext.Permissions.Select(p => p.CodeName).ToListAsync();

    public async Task AddRange(
        IEnumerable<Permission> permissions, CancellationToken cancellationToken = default)
    {
        await accountsContext.Permissions.AddRangeAsync(permissions, cancellationToken);
        await accountsContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<HashSet<string>> GetPermissionCodesByUserId(
        Guid userId, CancellationToken cancellationToken = default)
    {
        var perms = await accountsContext.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.Permissions)
            .Select(p => p.CodeName)
            .ToListAsync(cancellationToken);

        return perms.ToHashSet();
    }

    public async Task AddRolesWithPermissions(
        IEnumerable<Role> rolesWithPermissions, CancellationToken cancellationToken = default)
    {
        await accountsContext.Roles.AddRangeAsync(rolesWithPermissions, cancellationToken);
        await accountsContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ClearRolesAndPermissions(CancellationToken cancellationToken = default)
    {
        await accountsContext.Roles.ExecuteDeleteAsync(cancellationToken);
        await accountsContext.Permissions.ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken = default)
    {
        return await accountsContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }
}