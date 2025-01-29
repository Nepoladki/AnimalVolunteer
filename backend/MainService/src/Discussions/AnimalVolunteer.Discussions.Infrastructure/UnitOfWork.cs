using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Discussions.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace AnimalVolunteer.Discussions.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly WriteDbContext _context;

    public UnitOfWork(WriteDbContext context)
    {
        _context = context;
    }

    public async Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
