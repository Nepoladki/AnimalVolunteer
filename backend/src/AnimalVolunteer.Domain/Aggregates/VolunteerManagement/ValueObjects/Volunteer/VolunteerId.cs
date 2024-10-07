namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;

public record VolunteerId
{
    public Guid Value { get; }
    protected VolunteerId(Guid value)
    {
        Value = value;
    }
    public static VolunteerId Create() => new(Guid.NewGuid());
    public static VolunteerId CreateWithGuid(Guid value) => new(value);
    public static VolunteerId Empty() => new(Guid.Empty);

    public static implicit operator VolunteerId(Guid value) => new(value);

    public static implicit operator Guid(VolunteerId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}