using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects;

public record FullName
{
    public const int MAX_NAME_LENGTH = 25;
    public const string DB_COLUMN_FIRSTNAME = "first_name";
    public const string DB_COLUMN_PATRONYMIC = "patronymic";
    public const string DB_COLUMN_LASTNAME = "last_name";

    private FullName(string firstName, string? patronymic, string lastName)
    {
        FirstName = firstName;
        Patronymic = patronymic;
        LastName = lastName;
    }
    public string FirstName { get; } = null!;
    public string? Patronymic { get; }
    public string LastName { get; } = null!;
    public static Result<FullName, Error> Create(string firstName, string? patronymic, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(firstName));

        if (patronymic is not null && patronymic.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(patronymic));

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(lastName));

        return new FullName(firstName, patronymic, lastName);
    }
}
