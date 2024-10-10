using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;

namespace AnimalVolunteer.Application.DTOs.VolunteerManagement.Pet;

public record QueryPetDto(
    Guid Id,
    Guid VolunteerId,
    string Name,
    string Description,
    string Color,
    double Weight,
    double Height,
    Guid SpeciesId,
    Guid BreedId,
    string HealthDescription,
    bool IsVaccinated,
    bool IsNeutered,
    string Country,
    string City,
    string Street,
    string House,
    DateOnly BirthDate,
    CurrentStatus CurrentStatus);
