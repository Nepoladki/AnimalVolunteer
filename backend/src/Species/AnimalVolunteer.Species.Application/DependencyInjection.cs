using AnimalVolunteer.Core.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Species.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesApplication(
        this IServiceCollection services)
    {
        return services.AddCommands().AddQueries();
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
