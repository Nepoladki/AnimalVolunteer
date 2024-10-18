using AnimalVolunteer.Species.Application.Queries.GetFiltredPaginatedAllSpecies;

namespace AnimalVolunteer.Species.Web.Requests;

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
