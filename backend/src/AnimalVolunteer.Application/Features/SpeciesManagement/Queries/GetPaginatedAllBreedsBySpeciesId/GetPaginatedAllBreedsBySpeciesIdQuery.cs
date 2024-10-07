using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Queries.GetPaginatedAllBreedsBySpeciesId;

public record GetPaginatedAllBreedsBySpeciesIdQuery(
    Guid SpeciesId,
    int Page,
    int PageSize) : IQuery;

