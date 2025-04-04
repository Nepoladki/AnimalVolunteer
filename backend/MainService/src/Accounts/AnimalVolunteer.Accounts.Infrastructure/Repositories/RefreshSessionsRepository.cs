using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Infrastructure.DbContexts;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Accounts.Infrastructure.Repositories;

public class RefreshSessionsRepository : IRefreshSessionsRepository
{
    private readonly AccountsWriteDbContext _context;

    public RefreshSessionsRepository(AccountsWriteDbContext context)
    {
        _context = context;
    }

    public void DeleteSession(RefreshSession session)
    {
        _context.Remove(session);
    }

    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken)
    {
        var session = await _context.RefreshSessions
            .Include(x => x.User)
            .FirstOrDefaultAsync(rs => rs.RefreshToken == refreshToken, cancellationToken);

        if (session is null)
            return Errors.Accounts.RefreshSessionNotFound(refreshToken);

        return session;
    }
}

