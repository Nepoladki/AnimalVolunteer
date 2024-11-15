using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public sealed class BreedId : ValueObject
{
    public Guid Value { get; }
    private BreedId(Guid value)
    {
        Value = value;
    }
    public static BreedId Create() => new(Guid.NewGuid());
    public static BreedId CreateWithGuid(Guid value) => new(value);
    public static BreedId Empty() => new(Guid.Empty);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator BreedId(Guid value) => new(value);

    public static implicit operator Guid(BreedId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}
