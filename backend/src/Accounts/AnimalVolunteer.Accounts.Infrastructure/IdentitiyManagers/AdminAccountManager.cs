using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class AdminAccountManager
{
    private readonly AccountsDbContext _context;

    public AdminAccountManager(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task AddAdminAccount(
        AdminAccount adminAccount, CancellationToken cancellationToken = default)
    {
        await _context.AdminAccounts.AddAsync(adminAccount, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> AnyAdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await _context.AdminAccounts.AnyAsync(cancellationToken);
    }
}
