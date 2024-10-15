using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using FluentValidation;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Core.Validation;

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

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
    {
        return rule.WithMessage(error.Serialize());
    }
}
