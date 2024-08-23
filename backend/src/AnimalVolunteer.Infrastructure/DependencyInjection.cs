using AnimalVolunteer.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }
}
