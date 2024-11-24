using LinqToDB.Mapping;

namespace AnimalVolunteer.Core.DTOs.Discussions;
[Table("messages", Schema = "discussions")]
public record MessageDto
{
    [Column("id")] public Guid Id { get; }
    [Column("user_id")] public Guid UserId { get; }
    [Column("text")] public string Text { get; } = default!;
    [Column("created_at")] public DateTime CreatedAt { get; }
    [Column("is_edited")] public bool IsEdited { get; }

    [Association(ThisKey = "discussion_id", OtherKey = "id")]
    public DiscussionDto Discussion { get; } = null!;
}
