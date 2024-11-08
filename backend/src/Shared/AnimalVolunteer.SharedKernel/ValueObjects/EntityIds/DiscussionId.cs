namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;


public record DiscussionId
{
    private DiscussionId(Guid Id) => Value = Id;
    public Guid Value { get; }
    public static DiscussionId Create() => new(Guid.NewGuid());
    public static DiscussionId CreateWithGuid(Guid id) => new(id);
    public static DiscussionId CreateEmpty() => new(Guid.Empty);

    public static implicit operator DiscussionId(Guid value) => new(value);

    public static implicit operator Guid(DiscussionId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}

