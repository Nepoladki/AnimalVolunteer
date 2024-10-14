using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetsFilteredPaginated;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;

public record GetPetsFilteredPaginatedRequest(
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
    int PageSize)
{
    public GetPetsFilteredPaginatedQuery ToQuery() =>
        new(VolunteerId,
            SpeciesId,
            BreedId,
            Name,
            AgeFrom,
            AgeTo,
            Color,
            WeightFrom,
            WeightTo,
            HeightFrom,
            HeightTo,
            Country,
            City,
            SortBy,
            SortDirection,
            Page,
            PageSize);
}
