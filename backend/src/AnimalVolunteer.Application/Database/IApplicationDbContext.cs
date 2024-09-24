using AnimalVolunteer.Domain.Aggregates.Volunteer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AnimalVolunteer.Application.Database;

public interface IApplicationDbContext
{
    DbSet<Volunteer> Volunteers { get; }
    Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
