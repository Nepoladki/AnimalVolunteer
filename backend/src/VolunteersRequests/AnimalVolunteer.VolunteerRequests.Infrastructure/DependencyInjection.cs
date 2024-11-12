using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.VolunteerRequests.Infrastructure.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        services
            .AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.VolunteerRequests)
            .AddScoped<WriteDbContext>();

        return services;
    }
}

