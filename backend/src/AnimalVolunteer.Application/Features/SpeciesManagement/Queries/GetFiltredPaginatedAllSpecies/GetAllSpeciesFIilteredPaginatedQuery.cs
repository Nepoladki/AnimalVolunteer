using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Queries.GetAllSpecies;

public record GetAllSpeciesFIilteredPaginatedQuery(
    string? Name,
    int Page,
    int PageSize) : IQuery;
