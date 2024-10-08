﻿using AnimalVolunteer.Application.Features.SpeciesManagement.Queries.GetAllSpecies;

namespace AnimalVolunteer.API.Controllers.Species.Requests;

public record GetAllSpeciesFilteredPaginatedRequest(
    string? Name,
    int Page,
    int PageSize)
{
    public GetAllSpeciesFIilteredPaginatedQuery ToQuery()
    {
        return new(Name, Page, PageSize);
    }
}
