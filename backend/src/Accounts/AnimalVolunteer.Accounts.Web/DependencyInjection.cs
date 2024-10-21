using AnimalVolunteer.Accounts.Application;
using AnimalVolunteer.Accounts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Accounts.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAccountsApplication()
            .AddAccountsInfrastructure(config);
        return services;
    }
}
