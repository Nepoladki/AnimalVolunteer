using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Infrastructure.DbContexts;
using AnimalVolunteer.Discussions.Infrastructure.Linq2db;
using AnimalVolunteer.Discussions.Infrastructure.Repositories;
using LinqToDB;
using LinqToDB.AspNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Discussions.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        services
            .AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Discussions)
            .AddScoped<WriteDbContext>()
            .AddScoped<IDiscussionsRepository, DiscussionsRepository>()
            .AddScoped<IReadOnlyRepository, ReadOnlyRepository>()
            .ConfigureLinq2db(config);

        return services;
    }

    private static IServiceCollection ConfigureLinq2db(
        this IServiceCollection services, IConfiguration config)
    {
        var dbOptions = new DatabaseOptions();
        config.GetSection(DatabaseOptions.SECTION_NAME).Bind(dbOptions);

        services.AddLinqToDBContext<Linq2dbConnection>((provider, options) =>
            options.UsePostgreSQL(config.GetConnectionString(dbOptions.PostgresConnectionName)
                ?? throw new ApplicationException("Unable to get connection string")));

        services.AddScoped<Linq2dbConnection>();

        return services;
    }
}

