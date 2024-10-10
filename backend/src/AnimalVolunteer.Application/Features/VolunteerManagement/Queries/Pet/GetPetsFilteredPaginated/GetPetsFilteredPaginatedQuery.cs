using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetsFilteredPaginated;

public record GetPetsFilteredPaginatedQuery(
    Guid VolunteerId,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Name, 
    double? Age, 
    double? Weight, 
    double? Height,
    string? Country,
    string? City) : IQuery;
