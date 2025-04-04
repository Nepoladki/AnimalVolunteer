using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Core.DTOs.Accounts;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IAccountsRepository
{
    Task AddAdminAccount(AdminAccount adminAccount, CancellationToken cancellationToken = default);
    Task AddParticipantAccount(ParticipantAccount participantAccount, CancellationToken cancellationToken = default);
    Task<VolunteerAccount?> GerVolunteerAccountByUserId(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> AnyAdminAccountExists(CancellationToken cancellationToken = default);
}