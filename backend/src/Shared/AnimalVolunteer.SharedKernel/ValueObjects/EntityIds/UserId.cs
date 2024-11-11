namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public record UserId
{
    private UserId(Guid Id) => Value = Id;
    public Guid Value { get; }
    public static UserId Create() => new(Guid.NewGuid());
    public static UserId CreateWithGuid(Guid id) => new(id);
    public static UserId CreateEmpty() => new(Guid.Empty);

    public static implicit operator UserId(Guid value) => new(value);

    public static implicit operator Guid (UserId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}

