using AnimalVolunteer.Accounts.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
        })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AuthDbContext>();

        services.AddScoped<AuthDbContext>();

        return services;
    }
}
