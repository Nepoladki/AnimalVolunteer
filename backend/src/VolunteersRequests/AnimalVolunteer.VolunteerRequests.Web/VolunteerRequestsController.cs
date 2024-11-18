using AnimalVolunteer.Framework;
using AnimalVolunteer.VolunteerRequests.Application.Features.Commands.CreateRequest;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.VolunteerRequests.Web;

public class VolunteerRequestsController : ApplicationController
{
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> CreateRequest(
        [FromServices] CreateRequestHandler handler,
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var command = new CreateRequestCommand(userId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }
}

