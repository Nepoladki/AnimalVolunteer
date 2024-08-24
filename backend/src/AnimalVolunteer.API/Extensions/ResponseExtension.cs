using AnimalVolunteer.API.Response;
using AnimalVolunteer.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using static AnimalVolunteer.Domain.Common.Error;

namespace AnimalVolunteer.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var responseError = new ResponseError(error.Code, error.Message, null);

        var envelope = Envelope.Error([responseError]);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToValidationErrorResponse(this FluentValidation.Results.ValidationResult result)
    {
        if (result.IsValid)
            throw new InvalidOperationException("Cant reach to result");

        var errors = from validationError in result.Errors
                     let errorMessage = validationError.ErrorMessage
                     let error = Error.Deserialize(errorMessage)
                     select new ResponseError(error.Code, error.Message, validationError.PropertyName);

        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}
