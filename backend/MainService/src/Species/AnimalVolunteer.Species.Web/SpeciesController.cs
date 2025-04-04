﻿using AnimalVolunteer.Framework;
using AnimalVolunteer.Framework.Authorization;
using AnimalVolunteer.Species.Application.Commands.DeleteBreedById;
using AnimalVolunteer.Species.Application.Commands.DeleteSpeciesById;
using AnimalVolunteer.Species.Application.Queries.GetFiltredPaginatedAllSpecies;
using AnimalVolunteer.Species.Application.Queries.GetPaginatedAllBreedsBySpeciesId;
using AnimalVolunteer.Species.Web.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.Species.Web;
[Authorize]
public class SpeciesController : ApplicationController
{
    [Permission(Permissions.Species.Read)]
    [HttpGet]
    public async Task<IActionResult> GetAllSpecies(
        [FromQuery] GetAllSpeciesFilteredPaginatedRequest request,
        [FromServices] GetAllSpeciesFilteredPaginatedQueryHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var species = await handler.Handle(query, cancellationToken);

        return Ok(species);
    }

    [Permission(Permissions.Species.Read)]
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

    [Permission(Permissions.Species.HardDelete)]
    [HttpDelete("{speciesId:guid}/breeds/{breedId:guid}")]
    public async Task<IActionResult> DeleteBreed(
        [FromRoute] Guid speciesId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBreedByIdCommand(speciesId, breedId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [Permission(Permissions.Species.HardDelete)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSpecies(
        [FromRoute] Guid id,
        [FromServices] DeleteSpeciesByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpeciesByIdCommand(id);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }
}
