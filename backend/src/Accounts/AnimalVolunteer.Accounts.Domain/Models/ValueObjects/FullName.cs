using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Accounts.Domain.Models.ValueObjects;

public sealed class FullName
{

    public const int MAX_LENGTH = 25;

    private FullName() { }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Patronymic { get; init; }

    public FullName(string firstName, string lastName, string? patronymic = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }
};
