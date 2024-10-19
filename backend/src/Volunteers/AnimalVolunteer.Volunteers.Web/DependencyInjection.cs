using AnimalVolunteer.Volunteers.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Volunteers.Infrastructure;
using AnimalVolunteer.Volunteers.Contracts;

namespace AnimalVolunteer.Volunteers.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IVolunteersContract, VolunteersContract>()
            .AddVolunteersInfrastructure(configuration)
            .AddVolunteersApplication();
    }
}
