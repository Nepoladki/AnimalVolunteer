using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.DTOs.Common;
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

    public async Task<UserAccountDto> GetUserAccountInfoById(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.AdminAccount)
            .Include(u => u.VolunteerAccount)
            .Include(u => u.ParticipantAccount)
            .Where(u => u.Id == userId)
            .Select(u => new UserAccountDto(
                u.Id,
                u.ParticipantAccount.Id,
                u.VolunteerAccount.Id,
                u.AdminAccount.Id,
                new FullNameDto(
                    u.FullName.FirstName,
                    u.FullName.Patronymic,
                    u.FullName.LastName),
                u.Photo,
                u.VolunteerAccount.Expirience,
                new List<SocialNetworkDto>(),
                new List<PaymentDetailsDto>(),
                new List<CertificateDto>()))
            .FirstOrDefaultAsync(cancellationToken);

    }

}
