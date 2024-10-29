using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class AdminAccountManager
{
    private readonly AccountsDbContext _context;

    public AdminAccountManager(AccountsDbContext context)
    {
        _context = context;
    }

    public Task AddAdminAccount(AdminAccount adminAccount, CancellationToken cancellationToken = default)
    {
        _context.Admi
    }
}
