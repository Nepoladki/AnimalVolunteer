using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPetPhotos;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Create;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Delete;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.ContactInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.MainInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.PaymentDetails;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.SocialNetworks;
using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AnimalVolunteer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateVolunteerMainInfoHandler>();
        services.AddScoped<UpdateVolunteerSocialNetworksHandler>();
        services.AddScoped<UpdateVolunteerPaymentDetailsHandler>();
        services.AddScoped<UpdateVolunteerContactInfoHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<AddPetHandler>();
        services.AddScoped<AddPetPhotosHandler>();
        services.AddScoped<GetVolunteersWithPaginationHandler>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
