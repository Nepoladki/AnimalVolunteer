using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions.CQRS;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AnimalVolunteer.Species.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesApplication(
        this IServiceCollection services)
    {
        return services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); ;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
        .AddClasses(c => c
            .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
            
    }

    private static IServiceCollection AddQueries(this IServiceCollection services) 
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
        .AddClasses(c => c
            .AssignableToAny(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}
