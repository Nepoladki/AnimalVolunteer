using AnimalVolunteer.SharedKernel;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Accounts.Application.Extensions;

public static class IdentityErrorExtensions
{
    public static ErrorList ToDomainErrors(this IEnumerable<IdentityError> identityErrors)
    {
        var errors = from idError in identityErrors
                 let error = Error.Failure(idError.Code, idError.Description)
                 select error;

        return new ErrorList(errors);
    }
}
