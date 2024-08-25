using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;

public record FullName
{
    public const int MAX_NAME_LENGTH = 25;
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
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(firstName));

        if (surName is not null && surName.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(surName));

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(lastName));

        return new FullName(firstName, surName, lastName);
    }
}
