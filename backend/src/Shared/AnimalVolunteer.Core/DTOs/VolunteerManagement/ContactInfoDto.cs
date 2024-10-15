namespace AnimalVolunteer.Core.DTOs.VolunteerManagement;

public record ContactInfoDto(
    string PhoneNumber,
    string Name,
    string? Note);
