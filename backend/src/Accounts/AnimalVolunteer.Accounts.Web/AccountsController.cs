using AnimalVolunteer.Accounts.Application.Commands.LoginUser;
using AnimalVolunteer.Accounts.Application.Commands.RefreshUser;
using AnimalVolunteer.Accounts.Application.Commands.RegisterUser;
using AnimalVolunteer.Accounts.Application.Commands.UpdatePaymentDetails;
using AnimalVolunteer.Accounts.Application.Commands.UpdateSocialNetworks;
using AnimalVolunteer.Accounts.Application.Queries.GetUserInfo;
using AnimalVolunteer.Accounts.Web.Requests;
using AnimalVolunteer.Framework;
using AnimalVolunteer.Framework.Authorization;
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
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshUserRequest request,
        [FromServices] RefreshUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [Permission(Permissions.Accounts.Update)]
    [HttpPut("{userId:guid}/payment-details")]
    public async Task<IActionResult> UpdatePaymentDetails(
        [FromBody] UpdatePaymentDetailsRequest request,
        [FromServices] UpdatePaymentDetailsHandler handler,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(userId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [Permission(Permissions.Accounts.Update)]
    [HttpPut("{userId:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        [FromBody] UpdateSocialNetworksRequest request,
        [FromServices] UpdateSocialNetworksHandler handler,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(userId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [Permission(Permissions.Accounts.Read)]
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserAccountsInfo(
        [FromServices] GetUserInfoHandler handler,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserInfoQuery(userId);

        var handleResult = await handler.Handle(query, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }
}
