namespace AnimalVolunteer.Domain.Common;

public record VolunteerId
{
    public Guid Id { get; }

    protected VolunteerId(Guid id)
    {
        Id = id;
    }
    public static VolunteerId Create() => new(Guid.NewGuid());
    public static VolunteerId Empty() => new(Guid.Empty);
}