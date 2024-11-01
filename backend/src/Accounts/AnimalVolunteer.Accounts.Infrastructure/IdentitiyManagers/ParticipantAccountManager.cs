using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class ParticipantAccountManager : IParticipantAccountManager
{
    private readonly AccountsDbContext _context;

    public ParticipantAccountManager(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task AddParticipant(
        ParticipantAccount account, CancellationToken cancellationToken = default)
    {
        await _context.ParticipantsAccounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken); 
    }
}

