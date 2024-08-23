using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.ValueObjects.Volunteer;

public record FullName
{
    private FullName(string firstName, string? surName, string lastName)
    {
        FirstName = firstName;
        SurName = surName;
        LastName = lastName;
    }
    public string FirstName { get; } = null!;
    public string? SurName { get; }
    public string LastName { get; } = null!;
    public static Result<FullName, Error> Create(string firstName, string? surName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Errors.General.InvalidValue(nameof(firstName));

        if (surName is not null && surName.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Errors.General.InvalidValue(nameof(surName));

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Errors.General.InvalidValue(nameof(lastName));

        return new FullName(firstName, surName, lastName);
    }
}
