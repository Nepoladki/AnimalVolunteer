namespace AnimalVolunteer.Application.Features.Volunteer.AddPet;

public record AddPetCommand(
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
    string? House,
    DateOnly BirthDate,
    string CurrentStatus,
    IEnumerable<FileDto> Files);

public record FileDto(
    string Filename,
    Stream Content,
    bool IsMain);