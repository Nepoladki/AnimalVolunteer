using AnimalVolunteer.Domain.Common;
using FluentValidation.Results;

namespace AnimalVolunteer.Application.Extensions;

public static class ValidationExtensions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
                     let errorMessage = validationError.ErrorMessage
                     let error = Error.Deserialize(errorMessage)
                     select Error.Validation(error.Code, error.Message, validationError.PropertyName);

        return errors.ToList();
    }
}
