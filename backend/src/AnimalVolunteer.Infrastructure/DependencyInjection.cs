using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Infrastructure.Options;
using AnimalVolunteer.Infrastructure.Providers;
using AnimalVolunteer.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace AnimalVolunteer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddScoped<IFileProvider, MinioProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddMinio(configuration);

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
