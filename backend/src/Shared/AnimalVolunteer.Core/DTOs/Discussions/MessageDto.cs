using LinqToDB.Mapping;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Core.DTOs.Discussions;
[Table("messages", Schema = "discussions")]
public class MessageDto
{
    [Column("id")] public Guid Id { get; set; }
    [Column("user_id")] public Guid UserId { get; set; }
    [Column("text")] public string Text { get; set; } = default!;
    [Column("created_at")] public DateTime CreatedAt { get; set; }
    [Column("is_edited")] public bool IsEdited { get; set; }

    [Association(ThisKey = Id)] public DiscussionDto Discussion { get; set; }
}
