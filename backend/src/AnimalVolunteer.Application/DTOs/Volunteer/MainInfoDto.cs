namespace AnimalVolunteer.Application.DTOs.Volunteer;

public record MainInfoDto(
    FullNameDto FullName,
    string Email,
    string Description);
