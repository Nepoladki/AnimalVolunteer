using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;

public record Text(string Value) : IComparable<Text>
{
    public static Result<Text, Error> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Errors.General.InvalidValue(nameof(text));

        return new Text(text);
    }
    public int CompareTo(Text? other) => Value.CompareTo(other?.Value);
}

