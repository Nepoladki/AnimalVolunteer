using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class PermissonManager
{
    private readonly AccountsWriteDbContext _accountsDbContext;

    public PermissonManager(AccountsWriteDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }


    public async Task<Permission?> FindByCodeName(
        string codeName, CancellationToken cancellationToken)
    {
        return await _accountsDbContext.Permissions
        .FirstOrDefaultAsync(p => p.CodeName == codeName, cancellationToken);
    }

    public async Task AddPermissionsIfNotExists(IEnumerable<string> permissionCodeNames)
    {
        foreach (var permissionCodeName in permissionCodeNames)
        {
            var permissionExists = await _accountsDbContext.Permissions
                    .AnyAsync(p => p.CodeName == permissionCodeName);

            if (permissionExists)
                continue;

            _accountsDbContext.Permissions
                .Add(new Permission { CodeName = permissionCodeName });
        }

        await _accountsDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<string>?> GetUserPermissionsById(
        Guid userId, CancellationToken cancellationToken)
    {
        var user = await _accountsDbContext.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        return user?.Roles
            .SelectMany(r => r.RolePermissions.Select(rp => rp.Permission.CodeName));
    }
} 
