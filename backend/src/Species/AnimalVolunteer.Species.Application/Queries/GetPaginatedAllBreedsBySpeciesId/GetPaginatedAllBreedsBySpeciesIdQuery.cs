using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Species.Application.Queries.GetPaginatedAllBreedsBySpeciesId;

public record GetPaginatedAllBreedsBySpeciesIdQuery(
    Guid SpeciesId,
    int Page,
    int PageSize) : IQuery;

