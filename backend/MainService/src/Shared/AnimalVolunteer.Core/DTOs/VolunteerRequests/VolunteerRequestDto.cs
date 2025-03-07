﻿using AnimalVolunteer.SharedKernel.ValueObjects;
using LinqToDB.Mapping;

namespace AnimalVolunteer.Core.DTOs.VolunteerRequests;

[Table("volunteer_requests", Schema = "volunteer_requests")]
public class VolunteerRequestDto
{
    [PrimaryKey]
    [Column("id")]
    public Guid Id { get; set; } = default!;

    [Column("user_id")]
    public Guid UserId { get; set; } = default!;

    [Column("admin_id")]
    public Guid AdminId { get; set; } = default;

    [Column("discussion_id")]
    public Guid? DiscussionId { get; set; }

    [Column("status_enum")]
    public string Status { get; set; } = default!;

    [Column("rejection_comment")]
    public string? RejectionComment { get; set; }

    [Column("expirience_description")]
    public string ExpirienceDescription { get; set; } = string.Empty;
    
    [Column("passport")]
    public string Passport { get; set; } = string.Empty;

    [Column("last_rejection_at")]
    public DateTime? LastRejectionAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}

