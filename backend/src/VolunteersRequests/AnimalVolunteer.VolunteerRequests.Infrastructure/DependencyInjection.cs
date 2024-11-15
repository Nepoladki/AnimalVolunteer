using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Infrastructure.DbContexts;
using AnimalVolunteer.VolunteerRequests.Infrastructure.Repositories;
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
            .AddScoped<WriteDbContext>()
            .AddScoped<IVolunteerRequestsRepository, VolunteerRequestsRepository>();

        return services;
    }
}

