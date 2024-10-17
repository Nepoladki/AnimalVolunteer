using AnimalVolunteer.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.Framework;

[ApiController]
[Route("api/[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        return base.Ok(Envelope.Ok(value));
    }
}

