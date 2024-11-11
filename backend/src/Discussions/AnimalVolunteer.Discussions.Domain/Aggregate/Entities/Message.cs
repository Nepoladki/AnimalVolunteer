using AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

namespace AnimalVolunteer.Discussions.Domain.Aggregate.Entities;

public sealed class Message : CSharpFunctionalExtensions.Entity<MessageId>
{
    // EF Core ctor
    private Message() { }

    private Message(Guid userId, Text text)
    {
        UserId = userId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
        IsEdited = false;
    }

    public Guid UserId { get; }
    public Text Text { get; private set; } = default!;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public bool IsEdited { get; private set; }

    public static Message Create(Guid userId, Text text) => new(userId, text);

    internal void AmendText(Text newText)
    {
        Text = newText;
        IsEdited = true;
    }
}

