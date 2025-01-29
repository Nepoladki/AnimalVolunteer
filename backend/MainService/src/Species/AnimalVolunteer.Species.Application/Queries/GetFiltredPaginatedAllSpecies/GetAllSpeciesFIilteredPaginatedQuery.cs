using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Species.Application.Queries.GetFiltredPaginatedAllSpecies;

public record GetAllSpeciesFIilteredPaginatedQuery(
    string? Name,
    int Page,
    int PageSize) : IQuery;
