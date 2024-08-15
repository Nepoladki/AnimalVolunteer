namespace AnimalVolunteer.Domain.ValueObjects;

public record VolunteerId
{   
    private VolunteerId() { }
    public Guid Value { get; }

    protected VolunteerId(Guid value)
    {
        Value = value;
    }
    public static VolunteerId Create() => new(Guid.NewGuid());
    public static VolunteerId CreateWithGuid(Guid value) => new(value);
    public static VolunteerId Empty() => new(Guid.Empty);
}