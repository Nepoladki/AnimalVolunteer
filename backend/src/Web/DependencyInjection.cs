using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

namespace AnimalVolunteer.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration config)
    {
        AddSerilogLogger(services, config);

        services.AddJwtAuthentication(config);

        services.AddAuthorization();

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

    private static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "validIssuer",
                    ValidAudience = "validaudience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somebytes"))
                };
            });

        return services;
    }
}
