using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects;

public record Name
{
    public const int MAX_NAME_LENGTH = 25;
    public const string DB_COLUMN_NAME = "name";
    public string Value { get; } = default!;
    private Name(string value)
    {
        Value = value;
    }
    public static Result<Name, Error> Create(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(name));

        return new Name(name);
    }
}
