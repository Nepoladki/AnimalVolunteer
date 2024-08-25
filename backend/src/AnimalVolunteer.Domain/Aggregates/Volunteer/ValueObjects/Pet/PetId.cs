namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;

public record PetId
{
    public Guid Value { get; }
    private PetId(Guid value)
    {
        Value = value;
    }
    public static PetId Create() => new(Guid.NewGuid());
    public static PetId CreateWithGuid(Guid value) => new(value);
    public static PetId Empty() => new(Guid.Empty);

    public static implicit operator PetId(Guid id) => new(id);

    public static implicit operator Guid(PetId petId)
    {
        ArgumentNullException.ThrowIfNull(petId);

        return petId.Value;
    }
}