﻿using AnimalVolunteer.Species.Application.Queries.GetPaginatedAllBreedsBySpeciesId;

namespace AnimalVolunteer.Species.Web.Requests
{
    public record GetPaginatedAllBreedsBySpeciesIdRequest(
        int Page,
        int PageSize)
    {
        public GetPaginatedAllBreedsBySpeciesIdQuery ToQuery(Guid Id) =>
            new(Id, Page, PageSize);
    }
}