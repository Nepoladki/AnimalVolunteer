using System.Data;

namespace AnimalVolunteer.Species.Application;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
