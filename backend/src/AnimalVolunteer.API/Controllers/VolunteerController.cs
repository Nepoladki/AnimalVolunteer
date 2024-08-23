using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.API.Response;
using AnimalVolunteer.Application.Features.CreateVolunteer;
using AnimalVolunteer.Domain.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AnimalVolunteer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    private readonly CreateVolunteerHandler _createVolunteerService;

    public VolunteerController(CreateVolunteerHandler createVolunteerService)
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
