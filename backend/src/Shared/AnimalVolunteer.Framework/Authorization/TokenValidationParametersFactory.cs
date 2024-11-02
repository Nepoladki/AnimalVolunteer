using AnimalVolunteer.Core.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AnimalVolunteer.Accounts.Infrastructure;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters CreateWithLifetimeValidation(JwtOptions options)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            ClockSkew = TimeSpan.FromMinutes(options.ClockSkewMinutes),
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.SecretKey))
        };
    }

    public static TokenValidationParameters CreateWithoutLifetimeValidation(JwtOptions options)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            ClockSkew = TimeSpan.FromMinutes(options.ClockSkewMinutes),
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.SecretKey))
        };
    }
}

