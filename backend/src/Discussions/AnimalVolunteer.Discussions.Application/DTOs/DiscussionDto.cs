namespace AnimalVolunteer.Discussions.Application.DTOs;

public record DiscussionDto(
    IReadOnlyList<MessageDto> Messages,
    IReadOnlyList<Guid> UserIds,
    Guid RelationId,
    bool IsOpened);

