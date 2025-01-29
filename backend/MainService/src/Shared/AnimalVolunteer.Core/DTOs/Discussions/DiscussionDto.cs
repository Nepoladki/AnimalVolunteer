using LinqToDB.Mapping;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Core.DTOs.Discussions;

[Table("discussions", Schema = "discussions")]
public record DiscussionDto
{
    [Column("id"), PrimaryKey, NotNull] public Guid Id { get; set; }
    [Column("related_entity"), NotNull] public Guid RelatedId { get; set; }
    public IReadOnlyList<MessageDto> Messages { get; set; }
    [Column("users_ids"), NotNull] public Guid[] UsersIds { get; set; } = default!;
    [Column("is_opened"), NotNull] public bool IsOpened { get; set; }
}
