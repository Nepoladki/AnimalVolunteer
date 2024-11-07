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
using AnimalVolunteer.Core;
using AnimalVolunteer.Framework.Authorization;
using AnimalVolunteer.Accounts.Infrastructure.DbContexts;

namespace AnimalVolunteer.Accounts.Infrastructure;

public static partial class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.SECTION_NAME));
        services.Configure<AdminOptions>(config.GetSection(AdminOptions.SECTION_NAME));

        services.AddScoped<AccountsWriteDbContext>();
        services.AddScoped<IReadDbContext, AccountsReadDbContext>();

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Accounts);

        services.AddAuthenticationAndAuthorization(config);

        services.AddCustomIdentityManagers();

        services.AddAccountsPermissionsSeeding();

        return services;
    }

    private static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services, IConfiguration config)
    {
        var jwtOptions = config.GetSection(JwtOptions.SECTION_NAME).Get<JwtOptions>()
             ?? throw new ApplicationException("Missing JwtOptions configuration");

        services
            .AddIdentityCore<User>(options => options.User.RequireUniqueEmail = true)
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AccountsWriteDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                options.TokenValidationParameters = TokenValidationParametersFactory
                    .CreateWithLifetimeValidation(jwtOptions)); 

        services
            .AddAuthorization()
            .AddScoped<IJwtTokenProvider, JwtTokenProvider>()
            .AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        return services;
    }

    private static IServiceCollection AddCustomIdentityManagers(this IServiceCollection services)
    {
        services
            .AddScoped<IAccountManager, AccountManager>()
            .AddScoped<AccountManager>()
            .AddScoped<IRefreshSessionManager, RefreshSessionManager>()
            .AddScoped<PermissonManager>()
            .AddScoped<RolePermissionManager>();

        return services;
    }

    private static IServiceCollection AddAccountsPermissionsSeeding(this IServiceCollection services)
    {
        services
            .AddSingleton<AccountsSeeder>()
            .AddScoped<AccountsSeederService>();

        return services;
    }
}
