using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;

public class Text : CSharpFunctionalExtensions.ValueObject
{
    public const int MAX_TEXT_LENGTH = 1000;
    private Text(string value)
    {
        Value = value;
    }
    public string Value { get; }
    public static Result<Text, Error> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Errors.General.InvalidValue(nameof(text));

        return new Text(text);
    }
    public int CompareTo(Text? other) => Value.CompareTo(other?.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

