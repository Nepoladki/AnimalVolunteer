﻿namespace AnimalVolunteer.Domain.ValueObjects.Pet;

public record PetId
{
    public Guid Value { get; }
    protected PetId(Guid value)
    {
        Value = value;
    }
    public static PetId Create() => new(Guid.NewGuid());
    public static PetId CreateWithGuid(Guid value) => new(value);
    public static PetId Empty() => new(Guid.Empty);
}