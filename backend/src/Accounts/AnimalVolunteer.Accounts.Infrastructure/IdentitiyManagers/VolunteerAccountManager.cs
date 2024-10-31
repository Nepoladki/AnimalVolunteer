using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class VolunteerAccountManager : IVolunteerAccountManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public VolunteerAccountManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task<VolunteerAccount?> GerVolunteerAccountByUserId(
        Guid userId, CancellationToken cancellationToken)
    {
        return await _accountsDbContext.VolunteerAccounts
            .FirstOrDefaultAsync(v => v.UserId == userId, cancellationToken);
    }
}

