using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public sealed class VolunteerRequestId : ValueObject, IComparable<VolunteerRequestId>
{
    private VolunteerRequestId(Guid Id) => Value = Id;
    public Guid Value { get; }
    public static VolunteerRequestId Create() => new(Guid.NewGuid());
    public static VolunteerRequestId CreateWithGuid(Guid id) => new(id);
    public static VolunteerRequestId CreateEmpty() => new(Guid.Empty);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(VolunteerRequestId? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public static implicit operator VolunteerRequestId(Guid value) => new(value);

    public static implicit operator Guid(VolunteerRequestId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}

