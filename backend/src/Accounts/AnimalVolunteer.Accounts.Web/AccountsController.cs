using AnimalVolunteer.Accounts.Application.Commands.RegisterUser;
using AnimalVolunteer.Accounts.Web.Requests;
using AnimalVolunteer.Framework;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.Accounts.Web;

public partial class AccountsController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }
}
