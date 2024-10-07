using AnimalVolunteer.Application.Features.SpeciesManagement.Queries.GetPaginatedAllBreedsBySpeciesId;

namespace AnimalVolunteer.API.Controllers.Species.Requests
{
    public record GetPaginatedAllBreedsBySpeciesIdRequest(
        int Page,
        int PageSize)
    {
        public GetPaginatedAllBreedsBySpeciesIdQuery ToQuery(Guid Id) =>
            new(Id, Page, PageSize);
    }
}