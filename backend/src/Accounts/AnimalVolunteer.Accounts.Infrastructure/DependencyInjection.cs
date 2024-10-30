using AnimalVolunteer.Accounts.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AnimalVolunteer.Core.Options;
using Microsoft.Extensions.Configuration;
using AnimalVolunteer.Accounts.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using AnimalVolunteer.Accounts.Infrastructure.Providers;
using AnimalVolunteer.Accounts.Infrastructure.DatabaseSeeding;
using AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;
using AnimalVolunteer.Accounts.Infrastructure.Options;

namespace AnimalVolunteer.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        services
            .AddIdentityCore<User>(options =>
                {options.User.RequireUniqueEmail = true;})
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AccountsDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<JwtOptions>(config.GetSection(JwtOptions.SECTION_NAME));
        services.Configure<AdminOptions>(config.GetSection(AdminOptions.SECTION_NAME));

        services.AddScoped<AccountsDbContext>();

        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

        services.AddAuthenticationAndAuthorization(config);

        services.AddAccountsPermissionsSeeding();

        return services;
    }

    private static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services, IConfiguration config)
    {
        var jwtOptions = config.GetSection(JwtOptions.SECTION_NAME).Get<JwtOptions>()
             ?? throw new ApplicationException("Missing JwtOptions configuration");

        services
            .AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    ClockSkew = TimeSpan.FromMinutes(jwtOptions.ClockSkewMinutes),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

        services.AddAuthorization(); 

        return services;
    }

    private static IServiceCollection AddAccountsPermissionsSeeding(this IServiceCollection services)
    {
        services.AddScoped<IParticipantAccountManager, ParticipantAccountManager>();
        services.AddScoped<PermissonManager>();
        services.AddScoped<RolePermissionManager>();
        services.AddScoped<AdminAccountManager>();
        services.AddScoped<AccountsSeederService>();
        services.AddSingleton<AccountsSeeder>();

        return services;
    }
}
