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
    public static Result<FullName> Create(string firstName, string surName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<FullName>("Invalid first name");

        if (surName is not null && surName.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<FullName>("Invalid surname");

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<FullName>("Invalid last name");

        return Result.Success(new FullName(firstName, surName, lastName));
    }
}
