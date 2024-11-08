namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public class VolunteerRequestId : IComparable, IComparable<VolunteerRequestId>
{
    private VolunteerRequestId(Guid Id) => Value = Id;
    public Guid Value { get; }
    public static VolunteerRequestId Create() => new(Guid.NewGuid());
    public static VolunteerRequestId CreateWithGuid(Guid id) => new(id);
    public static VolunteerRequestId CreateEmpty() => new(Guid.Empty);

    public int CompareTo(object? obj) => Value.CompareTo(obj);

    public int CompareTo(VolunteerRequestId? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return other.CompareTo(Value);
    }

    public static implicit operator VolunteerRequestId(Guid value) => new(value);

    public static implicit operator Guid(VolunteerRequestId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}

