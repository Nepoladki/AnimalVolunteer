using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.BackgroundServices;
using AnimalVolunteer.Core.DTOs.Volunteers.Pet;
using AnimalVolunteer.Core.Files;
using AnimalVolunteer.Core.MessageQueues;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Volunteers.Application.Interfaces;
using AnimalVolunteer.Volunteers.Infrastructure.DbContexts;
using AnimalVolunteer.Volunteers.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace AnimalVolunteer.Volunteers.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
            .AddMinioVault(configuration)
            .AddRepositories();

        services.AddScoped<IFileProvider, MinioProvider>();
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Volunteers);

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

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SECTION_NAME));

        services.AddScoped<WriteDbContext>();
        services.AddKeyedTransient<WriteDbContext>(Modules.Volunteers);
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }

    private static IServiceCollection AddMinioVault(
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
