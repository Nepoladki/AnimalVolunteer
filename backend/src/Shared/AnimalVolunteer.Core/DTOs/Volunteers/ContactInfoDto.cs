namespace AnimalVolunteer.Core.DTOs.Volunteers;

public record ContactInfoDto(
    string PhoneNumber,
    string Name,
    string? Note);
