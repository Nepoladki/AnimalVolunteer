using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

namespace AnimalVolunteer.Discussions.Domain.Aggregate.Entities;

public sealed class Message : CSharpFunctionalExtensions.Entity<MessageId>
{
    // EF Core ctor
    private Message() { }
    public Guid UserId { get; }
    public string Text { get; } = default!;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public bool IsEdited { get; }
}

