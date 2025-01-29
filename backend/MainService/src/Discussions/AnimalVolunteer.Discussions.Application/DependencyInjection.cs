using AnimalVolunteer.Core.Abstractions.CQRS;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Discussions.Application;

public static class DependencyInjection
{
    private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();
    public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddQueries()
            .AddCommands()
            .AddValidatorsFromAssembly(_assembly);
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(_assembly)
            .AddClasses(c => c
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(_assembly)
            .AddClasses(c => c
                .AssignableTo(typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
}

