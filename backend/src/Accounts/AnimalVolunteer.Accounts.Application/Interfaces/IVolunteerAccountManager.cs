using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

namespace AnimalVolunteer.Accounts.Application.Interfaces;
public interface IVolunteerAccountManager
{
    Task<VolunteerAccount?> GerVolunteerAccountByUserId(Guid userId, CancellationToken cancellationToken);

    //Task UpdateVolunteerAccount(VolunteerAccount account, CancellationToken cancellationToken);
}
