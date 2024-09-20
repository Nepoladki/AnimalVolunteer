namespace AnimalVolunteer.API.Contracts;

public record AddPetRequest(
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
    string? House,
    DateOnly BirthDate,
    string CurrentStatus,
    IFormFileCollection Files);
