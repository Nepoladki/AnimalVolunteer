using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class AccountManager : IAccountManager
{
    private readonly AccountsDbContext _context;

    public AccountManager(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task AddAdminAccount(
        AdminAccount adminAccount, CancellationToken cancellationToken = default)
    {
        _context.AdminAccounts.Add(adminAccount);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> AnyAdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await _context.AdminAccounts.AnyAsync(cancellationToken);
    }

    public async Task<VolunteerAccount?> GerVolunteerAccountByUserId(
        Guid userId, CancellationToken cancellationToken)
    {
        return await _context.VolunteerAccounts
            .FirstOrDefaultAsync(v => v.UserId == userId, cancellationToken);
    }

    public async Task AddParticipantAccount(
        ParticipantAccount account, CancellationToken cancellationToken = default)
    {
        _context.ParticipantsAccounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
