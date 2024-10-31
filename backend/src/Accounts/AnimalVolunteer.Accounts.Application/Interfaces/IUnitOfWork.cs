using System.Data;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
