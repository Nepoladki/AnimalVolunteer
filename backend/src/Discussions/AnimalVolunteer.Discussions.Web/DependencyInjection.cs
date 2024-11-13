using AnimalVolunteer.Discussions.Application;
using AnimalVolunteer.Discussions.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Discussions.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscussionsModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure(configuration)
            .AddApplication(configuration);

        return services;
    }
}

