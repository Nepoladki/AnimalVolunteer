using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public sealed class VolunteerId : ValueObject
{
    public Guid Value { get; }
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    public static VolunteerId Create() => new(Guid.NewGuid());
    public static VolunteerId CreateWithGuid(Guid value) => new(value);
    public static VolunteerId Empty() => new(Guid.Empty);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator VolunteerId(Guid value) => new(value);

    public static implicit operator Guid(VolunteerId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}