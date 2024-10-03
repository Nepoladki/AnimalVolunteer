using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPetPhotos;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Create;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Delete;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.ContactInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.MainInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.PaymentDetails;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.SocialNetworks;
using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;
using AnimalVolunteer.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AnimalVolunteer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(c => c
            .AssignableTo(typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(c => c
            .AssignableTo(typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}
