﻿using AnimalVolunteer.Application.Features.Volunteer.AddPet;
using AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;
using AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;
using AnimalVolunteer.Application.Features.Volunteer.Delete;
using AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;
using AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;
using AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;
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

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
