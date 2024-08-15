namespace AnimalVolunteer.Domain.Common;

public record PetId
{
    public Guid Id { get; }

    protected PetId(Guid id)
    {
        Id = id;
    }

    public static PetId Create() => new(Guid.NewGuid());
    public static PetId Empty() => new(Guid.Empty);
}