﻿using AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;
using AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;
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
        services.AddScoped<UpdateVounteerMainInfoHandler>();
        services.AddScoped<UpdateVolunteerSocialNetworksHandler>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
