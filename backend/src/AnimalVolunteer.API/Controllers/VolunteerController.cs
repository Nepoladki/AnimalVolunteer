using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.Application.Requests;
using AnimalVolunteer.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.API.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    private readonly CreateVolunteerService _createVolunteerService;

    public VolunteerController(CreateVolunteerService createVolunteerService)
    {
        _createVolunteerService = createVolunteerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var creationResult = await _createVolunteerService.Create(request, cancellationToken);

        if (creationResult.IsFailure)
            return creationResult.Error.ToResponse();

        return Ok((Guid)creationResult.Value);
    }
}
