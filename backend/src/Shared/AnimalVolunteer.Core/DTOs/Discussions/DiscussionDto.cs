﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Core.DTOs.Discussions;

[Table("discussions", Schema = "discussions")]
public record DiscussionDto
{
    [Column("id")]public Guid Id { get; }
    [Column("related_entity")] public Guid RelationId { get; }
    [Column("messages")] public IReadOnlyList<MessageDto> Messages { get; }
    [Column("users_ids")] public IReadOnlyList<Guid> UsersIds { get; }
    [Column("is_opened")] public bool IsOpened { get; }
}