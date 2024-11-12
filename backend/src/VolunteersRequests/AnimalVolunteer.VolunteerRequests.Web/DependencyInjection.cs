using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.VolunteerRequests.Application;
using AnimalVolunteer.VolunteerRequests.Infrastructure;

namespace AnimalVolunteer.VolunteerRequests.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerRequestsModule(
        this IServiceCollection services, IConfiguration config)
    {
        services
            .AddApplication(config)
            .AddInfrastructure(config);

        return services;
    }
}

