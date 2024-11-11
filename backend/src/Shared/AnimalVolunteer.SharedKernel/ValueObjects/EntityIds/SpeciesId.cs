using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
public sealed class SpeciesId : ValueObject
{
    public Guid Value { get; }
    private SpeciesId(Guid value)
    {
        Value = value;
    }
    public static SpeciesId Create() => new(Guid.NewGuid());
    public static SpeciesId CreateWithGuid(Guid value) => new(value);
    public static SpeciesId Empty() => new(Guid.Empty);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator SpeciesId(Guid value) => new(value);

    public static implicit operator Guid(SpeciesId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}