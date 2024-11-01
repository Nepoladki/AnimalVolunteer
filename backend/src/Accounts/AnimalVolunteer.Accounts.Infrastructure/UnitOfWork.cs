using AnimalVolunteer.Accounts.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace AnimalVolunteer.Accounts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountsDbContext _context;

    public UnitOfWork(AccountsDbContext context)
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
