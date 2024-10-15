namespace AnimalVolunteer.Core.DTOs.VolunteerManagement;

public record MainInfoDto(
    FullNameDto FullName,
    string Email,
    string Description);
