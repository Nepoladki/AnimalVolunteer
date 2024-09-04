using AnimalVolunteer.Application.Features.Files.Delete;
using AnimalVolunteer.Application.Features.Files.Download;
using AnimalVolunteer.Application.Features.Files.Upload;
using AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;
using AnimalVolunteer.Application.Features.Volunteer.Delete;
using AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;
using AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;
using AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;
using AnimalVolunteer.Application.Interfaces;
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
        services.AddScoped<UpdateVolunteerPaymentDetailsHandler>();
        services.AddScoped<UpdateVolunteerContactInfoHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<UploadFileHandler>();
        services.AddScoped<DownloadFileHandler>();
        services.AddScoped<DeleteFileHandler>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
