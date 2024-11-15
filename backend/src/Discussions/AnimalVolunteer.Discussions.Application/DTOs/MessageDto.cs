namespace AnimalVolunteer.Discussions.Application.DTOs;

public record MessageDto(
    Guid UserId,
    string Text,
    DateTime CreatedAt,
    bool IsEdited);

