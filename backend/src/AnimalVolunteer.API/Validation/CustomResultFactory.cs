using AnimalVolunteer.API.Response;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using Error = AnimalVolunteer.Domain.Common.Error;

namespace AnimalVolunteer.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context, 
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
            throw new InvalidOperationException("ValidationProblemDetails is null");

        List<ResponseError> allErrors = []; 

        foreach (var (fieldName, validationErrors) in validationProblemDetails.Errors)
        {
            var errors = from errorMessage in validationErrors
                         let error = Error.Deserialize(errorMessage)
                         select new ResponseError(error.Code, error.Message, fieldName);

            allErrors.AddRange(errors);
        }

        var envelope = Envelope.Error(allErrors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}
