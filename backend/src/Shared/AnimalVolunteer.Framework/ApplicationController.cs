using AnimalVolunteer.API.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

