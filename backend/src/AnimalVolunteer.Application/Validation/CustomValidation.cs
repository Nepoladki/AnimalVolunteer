using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.Application.Validation;

public static class CustomValidation
{
    // Extension method which allow us to call value object's factory method to validate it in FluenValidation 
   public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
       this IRuleBuilder<T, TElement> ruleBuilder, 
       Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }
}
