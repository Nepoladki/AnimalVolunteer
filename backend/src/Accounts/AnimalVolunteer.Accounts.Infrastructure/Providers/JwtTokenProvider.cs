using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Application.Models;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Infrastructure.DbContexts;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Framework.Authorization;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimalVolunteer.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly AccountsWriteDbContext _accountsDbContext;

    public JwtTokenProvider(
        IOptions<JwtOptions> jwtOptions, AccountsWriteDbContext accountsDbContext)
    {
        _jwtOptions = jwtOptions.Value;
        _accountsDbContext = accountsDbContext;
    }

    public JwtResult GenerateAccessToken(User user)
    {
        var jti = Guid.NewGuid();

        Claim[] claims = 
            [
                new Claim(JwtClaimTypes.ID, user.Id.ToString()),
                new Claim(JwtClaimTypes.JTI, jti.ToString()) 
            ];

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var signingCreds = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        var configuredToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiringMinutes),
            signingCredentials: signingCreds);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(configuredToken);

        return new JwtResult(jwtToken, jti);
    }

    public async Task<Guid> GenerateRefreshToken(User user, Guid jti, CancellationToken cancellationToken)
    {
        var refreshSession = new RefreshSession
        {
            User = user,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(_jwtOptions.RefreshExpiringHours),
            RefreshToken = Guid.NewGuid(),
            Jti = jti,
        };

        _accountsDbContext.RefreshSessions.Add(refreshSession);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);

        return refreshSession.RefreshToken;
    }

    public async Task<Result<IReadOnlyList<Claim>, Error>> GetClaimsFromToken(string jwtToken)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var validationParameters = TokenValidationParametersFactory
            .CreateWithoutLifetimeValidation(_jwtOptions);

        var validationResult = await jwtHandler.ValidateTokenAsync(jwtToken, validationParameters);
        if (validationResult.IsValid == false)
            return Errors.Authentication.InvalidToken();

        return validationResult.ClaimsIdentity.Claims.ToList();
    }
}
