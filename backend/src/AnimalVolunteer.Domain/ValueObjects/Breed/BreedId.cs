namespace AnimalVolunteer.Domain.ValueObjects.Breed;

public record BreedId
{
    public Guid Value { get; }
    protected BreedId(Guid value)
    {
        Value = value;
    }
    public static BreedId Create() => new(Guid.NewGuid());
    public static BreedId CreateWithGuid(Guid value) => new(value);
    public static BreedId Empty() => new(Guid.Empty);

    public static implicit operator BreedId(Guid value) => new(value);

    public static implicit operator Guid(BreedId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}
