using AnimalVolunteer.Accounts.Application;
using AnimalVolunteer.Accounts.Contracts;
using AnimalVolunteer.Accounts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Accounts.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddScoped<IAccountsContract, AccountsContract>()
            .AddAccountsApplication()
            .AddAccountsInfrastructure(config);
        return services;
    }
}
