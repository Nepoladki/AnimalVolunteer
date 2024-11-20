using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Infrastructure.DbContexts;
using AnimalVolunteer.VolunteerRequests.Infrastructure.Linq2db.Connections;
using AnimalVolunteer.VolunteerRequests.Infrastructure.Repositories;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Mapping;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
        var dbOptions = new DatabaseOptions();
        config.GetSection(DatabaseOptions.SECTION_NAME).Bind(dbOptions);

        services.AddLinqToDBContext<Linq2DbConnection>((provider, options) =>
            options.UsePostgreSQL(config.GetConnectionString(dbOptions.PostgresConnectionName)
                ?? throw new ApplicationException("Unable to get connection string")));

        services.AddScoped<Linq2DbConnection>();

        return services;
    }
}

