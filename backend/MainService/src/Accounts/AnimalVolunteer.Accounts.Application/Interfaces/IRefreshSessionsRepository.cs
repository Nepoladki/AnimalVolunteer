using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IRefreshSessionsRepository
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);
    void DeleteSession(RefreshSession session);
}