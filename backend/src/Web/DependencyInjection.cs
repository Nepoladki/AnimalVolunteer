using Serilog;
using Serilog.Events;

namespace AnimalVolunteer.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration config)
    {
        AddSerilogLogger(services, config);

        return services;
    }

    public static void AddSerilogLogger(this IServiceCollection services, IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.Debug()
           .WriteTo.Seq(config.GetConnectionString("Seq")
               ?? throw new ArgumentNullException("Seq connection string was not found"))
           .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Information)
           .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Information)
           .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Information)
           .CreateLogger();

        services.AddSerilog();
    }
}
