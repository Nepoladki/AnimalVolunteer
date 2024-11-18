using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Infrastructure.DbContexts;
using AnimalVolunteer.VolunteerRequests.Infrastructure.Linq2db.Connections;
using AnimalVolunteer.VolunteerRequests.Infrastructure.Repositories;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Mapping;
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
            .AddScoped<IVolunteerRequestsRepository, VolunteerRequestsRepository>()
            .ConfigureLinq2db(config)
            .AddScoped<IReadOnlyRepository, ReadOnlyRepository>();

        return services;
    }

    private static IServiceCollection ConfigureLinq2db(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddLinqToDBContext<Linq2DbConnection>((provider, options) =>
        {
            options.UsePostgreSQL(config.GetConnectionString("Postgres")!);
            options.UseDefaultLogging(provider);
            return options;
        });

        return services;
    }
}

