using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly AccountsWriteDbContext _accountsContext;

    public AccountsRepository(AccountsWriteDbContext accountsContext)
    {
        _accountsContext = accountsContext;
    }

    public async Task AddAdminAccount(
        AdminAccount adminAccount,
        CancellationToken cancellationToken = default)
    {
        await _accountsContext.AdminAccounts.AddAsync(adminAccount, cancellationToken);
    }

    public async Task<bool> AnyAdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await _accountsContext.AdminAccounts.AnyAsync(cancellationToken);
    }

    public async Task<VolunteerAccount?> GerVolunteerAccountByUserId(
        Guid userId, CancellationToken cancellationToken)
    {
        return await _accountsContext.VolunteerAccounts
            .FirstOrDefaultAsync(v => v.UserId == userId, cancellationToken);
    }

    public async Task AddParticipantAccount(
        ParticipantAccount account, CancellationToken cancellationToken = default)
    {
        _accountsContext.ParticipantsAccounts.Add(account);
        await _accountsContext.SaveChangesAsync(cancellationToken);
    }
}
