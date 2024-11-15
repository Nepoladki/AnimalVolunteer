using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public sealed class AdminId : ValueObject
{
    private AdminId(Guid Id) => Value = Id;
    public Guid Value { get; }
    public static AdminId Create() => new(Guid.NewGuid());
    public static AdminId CreateWithGuid(Guid id) => new(id);
    public static AdminId CreateEmpty() => new(Guid.Empty);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator AdminId(Guid value) => new(value);

    public static implicit operator Guid(AdminId valueObj)
    {
        ArgumentNullException.ThrowIfNull(valueObj);
        return valueObj.Value;
    }
}

