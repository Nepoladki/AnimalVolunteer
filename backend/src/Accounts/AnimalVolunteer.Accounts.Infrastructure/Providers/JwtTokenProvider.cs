using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Framework.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimalVolunteer.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateAccessToken(
        User user, CancellationToken cancellationToken)
    {
        Claim[] claims = 
        [ new Claim(JwtClaimTypes.ID, user.Id.ToString()) ];

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

        return jwtToken;
       
    }
}
