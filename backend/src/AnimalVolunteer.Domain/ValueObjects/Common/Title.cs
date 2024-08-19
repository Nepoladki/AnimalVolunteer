using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.Domain.ValueObjects.Common;

public record Title
{
    private Title(string value)
    {
        Value = value;
    }
    public string Value { get; } = null!;

    public static Result<Title> Create(string value)
    {
        if (value is null || value.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<Title>("Invalid title");

        return Result.Success(new Title(value));
    }
}
