using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Species.Application;
using AnimalVolunteer.Species.Infrastructure.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;

namespace AnimalVolunteer.Species.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddRepositories();

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Species);

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        return services.AddScoped<ISpeciesRepository, SpeciesRepository>();
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(
            configuration.GetSection(DatabaseOptions.SECTION_NAME));

        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
}
