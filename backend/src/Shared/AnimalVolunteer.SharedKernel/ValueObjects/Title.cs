using CSharpFunctionalExtensions;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.SharedKernel.ValueObjects;

public record Title
{
    public const int MAX_LENGTH = 30;
    private Title(string value)
    {
        Value = value;
    }
    public string Value { get; } = null!;

    public static Result<Title, Error> Create(string value)
    {
        if (value is null || value.Length > MAX_LENGTH)
            return Errors.General.InvalidValue(nameof(Title));

        return new Title(value);
    }
}
