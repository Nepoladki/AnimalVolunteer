using AnimalVolunteer.Species.Application;
using AnimalVolunteer.Species.Contracts;
using AnimalVolunteer.Species.Infrastructure;
using AnimalVolunteer.Volunteers.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Species.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ISpeciesContract, SpeciesContract>()
            .AddSpeciesInfrastructure(configuration)
            .AddSpeciesApplication();
    }
}
