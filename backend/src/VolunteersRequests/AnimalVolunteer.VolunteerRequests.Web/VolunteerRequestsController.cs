using AnimalVolunteer.Framework;
using AnimalVolunteer.VolunteerRequests.Application.Features.Commands.CreateRequest;
using AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForConsideration;
using AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForReConsideration;
using AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequest;
using AnimalVolunteer.VolunteerRequests.Web.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.VolunteerRequests.Web;

public class VolunteerRequestsController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> CreateRequest(
        [FromBody] CreateRequestRequest request,
        [FromServices] CreateRequestHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateRequestCommand(request.UserId, request.VolunteerInfo);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpPut("{requestId:guid}/update")]
    public async Task<IActionResult> UpdateRequest(
        [FromRoute] Guid requestId,
        [FromBody] UpdateRequestRequest request,
        [FromServices] UpdateRequestHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRequestCommand(requestId, request.VolunteerInfo);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpPut("{requestId:guid}/considerate")]
    public async Task<IActionResult> TakeOnConsideration(
        [FromRoute] Guid requestId,
        [FromBody] TakeRequestForConsiderationRequest request,
        [FromServices] TakeRequestForConsiderationHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new TakeRequestForConsiderationCommand(requestId, request.AdminId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpPut("{requestId:guid}/re-considerate")]
    public async Task<IActionResult> TakeOnReConsideration(
        [FromRoute] Guid requestId,
        [FromServices] TakeRequestForReConsiderationHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new TakeRequestForReConsiderationCommand(requestId);



        return Ok();
    }
}

