using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IParticipantAccountManager
{
    Task AddParticipant(
        ParticipantAccount account, CancellationToken cancellationToken = default);
}