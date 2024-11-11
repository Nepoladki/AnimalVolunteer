
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public sealed class DiscussionId : ValueObject
{
    private DiscussionId(Guid id)
    {
        Value = id;
    }
    public Guid Value { get; }
    public static DiscussionId Create() => new(Guid.NewGuid());
    public static DiscussionId CreateWithGuid(Guid id) => new(id);
    public static DiscussionId CreateEmpty() => new(Guid.Empty);
    public int CompareTo(DiscussionId? other)
    {
        ArgumentNullException.ThrowIfNull(other, nameof(other));

        return Value.CompareTo(other);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator DiscussionId(Guid guid) => new(guid);

    public static implicit operator Guid(DiscussionId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}

