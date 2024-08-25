using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;

public record NameAndDescription
{
    public const int MAX_NAME_LENGTH = 25;
    public const int MAX_DESC_LENGTH = 500;
    public string Name { get; } = default!;
    public string Description { get; } = default!;
    private NameAndDescription(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public static Result<NameAndDescription, Error> Create(string name, string description)
    {
        if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(name));

        if (string.IsNullOrEmpty(description) || description.Length > MAX_DESC_LENGTH)
            return Errors.General.InvalidValue(nameof(description));

        return new NameAndDescription(name, description);
    }
}
