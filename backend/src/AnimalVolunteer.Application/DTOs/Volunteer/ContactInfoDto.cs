namespace AnimalVolunteer.Application.DTOs.Volunteer;

public record ContactInfoDto(
    string PhoneNumber,
    string Name,
    string? Note);
