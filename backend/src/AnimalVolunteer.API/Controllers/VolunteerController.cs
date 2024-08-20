using AnimalVolunteer.Application.Requests;
using AnimalVolunteer.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.API.Controllers;

[Route("[Controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    private readonly CreateVolunteerService _createVolunteerService;

    public VolunteerController(CreateVolunteerService createVolunteerService)
    {
        _createVolunteerService = createVolunteerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVolunteerRequest request)
    {
        var creationResult = await _createVolunteerService.Create(request);

        if (creationResult.IsFailure)
            return BadRequest(creationResult.Error);

        return Ok(creationResult.Value);
    }
}
