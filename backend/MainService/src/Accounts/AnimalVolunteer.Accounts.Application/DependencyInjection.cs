using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Core.Abstractions.CQRS;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AnimalVolunteer.Accounts.Application;

public static class DependencyInjection
{
    private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();
    public static IServiceCollection AddAccountsApplication(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(_assembly)
            .AddCommands()
            .AddQueries();

        return services;
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
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(c => c
                .AssignableTo(typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
}
