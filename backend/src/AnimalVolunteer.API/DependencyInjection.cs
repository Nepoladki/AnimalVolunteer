using AnimalVolunteer.API.Validation;
using FluentValidation.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace AnimalVolunteer.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configBuilder =>
        {
            configBuilder.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        return services;
    }
}
