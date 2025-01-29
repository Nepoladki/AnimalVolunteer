using AnimalVolunteer.Discussions.Application.Features.Queries.GetDiscussionById;
using AnimalVolunteer.Discussions.Application.Features.Queries.GetDiscussionByRelatedId;
using AnimalVolunteer.Framework;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.Discussions.Web;

public class DiscussionsController : ApplicationController
{
    [HttpGet("related-id/{relatedId:guid}")]
    public async Task<IActionResult> GetDiscussionByRelatedId(
        [FromRoute] Guid relatedId,
        [FromServices] GetDiscussionByRelatedIdHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new GetDiscussionByRelatedIdCommand(relatedId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }
}

