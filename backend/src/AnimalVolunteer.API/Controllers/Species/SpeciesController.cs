using AnimalVolunteer.Application.Features.SpeciesManagement.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.API.Controllers.Species;

public class SpeciesController : ApplicationController
{
    [HttpGet("species")]
    public async Task<IActionResult> GetAllSpecies(
        [FromServices] GetAllSpeciesQueryHandler handler,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
