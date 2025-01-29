using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

public class PetId : ValueObject
{
    public Guid Value { get; }
    private PetId(Guid value)
    {
        Value = value;
    }
    public static PetId Create() => new(Guid.NewGuid());
    public static PetId CreateWithGuid(Guid value) => new(value);
    public static PetId Empty() => new(Guid.Empty);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator PetId(Guid id) => new(id);

    public static implicit operator Guid(PetId petId)
    {
        ArgumentNullException.ThrowIfNull(petId);

        return petId.Value;
    }
}