using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Infrastructure.DbContexts;
using AnimalVolunteer.Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace AnimalVolunteer.Accounts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountsWriteDbContext _context;

    public UnitOfWork(AccountsWriteDbContext context)
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
