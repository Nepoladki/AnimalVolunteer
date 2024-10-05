using AnimalVolunteer.API.Controllers.Species.Requests;
using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.Application.Features.SpeciesManagement.Commands.DeleteBreedById;
using AnimalVolunteer.Application.Features.SpeciesManagement.Queries.GetAllSpecies;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.API.Controllers.Species;

public class SpeciesController : ApplicationController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllSpeciesFilteredPaginatedRequest request,
        [FromServices] GetAllSpeciesFilteredPaginatedQueryHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var species = await handler.Handle(query, cancellationToken);

        return Ok(species);
    }
    [HttpGet("{id:guid}/breeds")]
    public async Task<IActionResult> GetBreedsBySpecies(
        [FromRoute] Guid id,
        [FromQuery] GetPaginatedAllBreedsBySpeciesIdRequest request,
        [FromServices] GetPaginatedAllBreedsBySpeciesIdHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery(id);

        var handleResult = await handler.Handle(query, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToErrorList().ToResponse();

        return Ok(handleResult.Value);
    }
    [HttpDelete("{speciesId:guid}/breeds/{breedId:guid}")]
    public async Task<IActionResult> DeleteBreed(
        [FromRoute] Guid speciesId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new DeleteBreedByIdCommand(speciesId, breedId);

        var handleResult = await handler.Handle(query, cancellationToken);
    }


}
