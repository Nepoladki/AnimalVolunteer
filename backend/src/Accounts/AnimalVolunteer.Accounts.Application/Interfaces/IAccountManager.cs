using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IAccountManager
{
    Task AddAdminAccount(AdminAccount adminAccount, CancellationToken cancellationToken = default);
    Task AddParticipantAccount(ParticipantAccount participantAccount, CancellationToken cancellationToken = default);
    Task<VolunteerAccount?> GerVolunteerAccountByUserId(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> AnyAdminAccountExists(CancellationToken cancellationToken = default);
    Task GetUserAccountInfoById(Guid userId, CancellationToken cancellationToken);
}