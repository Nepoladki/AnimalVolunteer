using AnimalVolunteer.API.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AnimalVolunteer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApplicationController : ControllerBase 
{
    public override OkObjectResult Ok(object? value)
    {
        return base.Ok(Envelope.Ok(value));
    }
}

