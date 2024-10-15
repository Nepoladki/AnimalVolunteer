using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Infrastructure.BackgroundServices;
using AnimalVolunteer.Infrastructure.MessageQueues;
using AnimalVolunteer.Infrastructure.Options;
using AnimalVolunteer.Volunteers.Infrastructure;
using AnimalVolunteer.Volunteers.Infrastructure.DbContexts;
using AnimalVolunteer.Volunteers.Infrastructure.Files;
using AnimalVolunteer.Volunteers.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace AnimalVolunteer.Volunteers.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
            .AddMinio(configuration)
            .AddRepositories();

        services.AddScoped<IFileProvider, MinioProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<FileCleanerBackgroundService>();
        services.AddScoped<IFilesCleanerService, FilesCleanerService>();
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfoDto>>,
            InMemoryMessageQueue<IEnumerable<FileInfoDto>>>();

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SECTION_NAME));

        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.SECTION_NAME));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.SECTION_NAME).Get<MinioOptions>()
                ?? throw new ApplicationException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);

            options.WithCredentials(minioOptions.Username, minioOptions.Password);

            options.WithSSL(minioOptions.EnableSsl);
        });

        return services;
    }
}
