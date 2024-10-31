using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Core.DTOs.Volunteers;

public record MainInfoDto(
    FullNameDto FullName,
    string Email,
    string Description);
