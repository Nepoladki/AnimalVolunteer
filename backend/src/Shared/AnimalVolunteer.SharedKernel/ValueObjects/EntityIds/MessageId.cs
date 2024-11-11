﻿namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
public class MessageId : IComparable<MessageId>
{
    private MessageId(Guid value) => Value = value;
    public Guid Value { get; }
    public static MessageId Create() => new(Guid.NewGuid());
    public static MessageId CreateWithGuid(Guid id) => new(id);
    public static MessageId CreateEmpty() => new(Guid.Empty);

    public int CompareTo(MessageId? other)
    {
        ArgumentNullException.ThrowIfNull(other, nameof(other));

        return Value.CompareTo(other.Value);
    }

    public static implicit operator Guid(MessageId value) => value.Value;

    public static implicit operator MessageId(Guid guid) => new(guid);
}