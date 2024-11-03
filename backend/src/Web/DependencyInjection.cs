using AnimalVolunteer.Core.Options;
using AnimalVolunteer.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;

namespace AnimalVolunteer.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration config)
    {
        AddSerilogLogger(services, config);

        services.AddCustomSwaggerGen();

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

    private static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AnimalVolunteer API",
                Version = "1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insert JWT token value",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement

            { 
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
}
