using AnimalVolunteer.Accounts.Application.Models;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using System.Security.Claims;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IJwtTokenProvider
{
    JwtResult GenerateAccessToken(User user);
    Task<Guid> GenerateRefreshToken(User user, Guid jti, CancellationToken cancellationToken);
    Task<Result<IReadOnlyList<Claim>, Error>> GetClaimsFromToken(string jwtToken);
}
