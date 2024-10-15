using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetsFilteredPaginated;

public record GetPetsFilteredPaginatedQuery(
    Guid? VolunteerId,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Name,
    int? AgeFrom,
    int? AgeTo,
    string? Color,
    int? WeightFrom,
    int? WeightTo,
    int? HeightFrom,
    int? HeightTo,
    string? Country,
    string? City,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;
