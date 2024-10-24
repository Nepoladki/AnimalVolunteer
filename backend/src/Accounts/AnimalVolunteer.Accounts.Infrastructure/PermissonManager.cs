using AnimalVolunteer.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure;

public class PermissonManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public PermissonManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }


    public async Task<Permission?> FindByCodeName(string codeName) =>
        await _accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.CodeName == codeName);
    
    public async Task AddPermissionsIfNotExists(IEnumerable<string> permissionCodeNames)
    {
        foreach (var permissionCodeName in permissionCodeNames)
        {
            var permissionExists = await _accountsDbContext.Permissions
                    .AnyAsync(p => p.CodeName == permissionCodeName);

            if (permissionExists)
                return;

            await _accountsDbContext.Permissions
                .AddAsync(new Permission { CodeName = permissionCodeName });
        }

        await _accountsDbContext.SaveChangesAsync();
    }
}
