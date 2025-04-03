using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FileService.Communication;
public static class FileServiceExtensions
{
    public static IServiceCollection AddFileHttpCommunication(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileServiceOptions>(configuration.GetSection(FileServiceOptions.SECTION_NAME));

        services.AddHttpClient<FileHttpClient>((sp, config) =>
        {
            var serviceOptions = sp.GetRequiredService<IOptions<FileServiceOptions>>().Value;

            config.BaseAddress = new Uri(serviceOptions.Url);
        });

        return services;
    }
}
