using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;

public partial record Email
{
    public const int MAX_LENGTH = 50;
    public const string DB_COLUMN_NAME = "email";
    private const string EMAIL_REGEX_PATTERN = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
    
    [GeneratedRegex(EMAIL_REGEX_PATTERN)]
    private static partial Regex EmailRegex();
    public string Value { get; } = null!;
    private Email(string value) => Value = value;
    public static Result<Email, Error> Create(string email)
    {
        if (email.Length > MAX_LENGTH || !EmailRegex().IsMatch(email))
            return Errors.General.InvalidValue(nameof(email));

        return new Email(email);
    }
}
