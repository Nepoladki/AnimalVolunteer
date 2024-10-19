using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects;

public record Description
{
    public const int MAX_DESC_LENGTH = 1000;
    public const string DB_COLUMN_NAME = "description";
    private Description(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;
    public static Result<Description, Error> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESC_LENGTH)
            return Errors.General.InvalidValue(nameof(description));

        return new Description(description);
    }
}
