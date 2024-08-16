﻿namespace AnimalVolunteer.Domain.ValueObjects.Species;
public record SpeciesId
{
    public Guid Value { get; }

    protected SpeciesId(Guid value)
    {
        Value = value;
    }
    public static SpeciesId Create() => new(Guid.NewGuid());
    public static SpeciesId CreateWithGuid(Guid value) => new(value);
    public static SpeciesId Empty() => new(Guid.Empty);
}