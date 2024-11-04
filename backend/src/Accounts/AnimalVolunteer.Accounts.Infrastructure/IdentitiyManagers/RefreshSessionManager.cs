using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

public class RefreshSessionManager : IRefreshSessionManager
{
    private readonly AccountsDbContext _context;

    public RefreshSessionManager(AccountsDbContext context)
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

