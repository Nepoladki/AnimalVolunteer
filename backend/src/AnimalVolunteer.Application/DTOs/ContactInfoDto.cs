namespace AnimalVolunteer.Application.DTOs;

public record ContactInfoDto(
    string PhoneNumber,
    string Name,
    string? Note);
