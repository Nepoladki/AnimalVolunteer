using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AnimalVolunteer.Domain.Common.ValueObjects;

public partial record ContactInfo
{
    public const int MAX_PHONE_LENGTH = 16;
    public const int MAX_NAME_LENGTH = 50;
    public const int MAX_NOTE_LENGTH = 500;
    public const string DB_COLUMN_NAME = "contact_info";
    private const string REGEX_PATTERN_PHONE_NUMBER = @"^[+]?[0-9]([-\s\.]?[0-9]{3}){2}([-\s\.]?[0-9]{2}){2}$";

    [GeneratedRegex(REGEX_PATTERN_PHONE_NUMBER)]
    private static partial Regex PhoneNumberRegex();
    private ContactInfo(string phoneNumber, string name, string? note)
    {
        PhoneNumber = phoneNumber;
        Name = name;
        Note = note;
    }
    public string PhoneNumber { get; } = null!;
    public string Name { get; } = null!;
    public string? Note { get; }
    public static Result<ContactInfo, Error> Create(string phoneNumber, string name, string? note)
    {
        if (!PhoneNumberRegex().IsMatch(phoneNumber))
            return Errors.General.InvalidValue(nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(name));

        if (note is not null && note.Length > MAX_NOTE_LENGTH)
            return Errors.General.InvalidValue(nameof(note));

        return new ContactInfo(phoneNumber, name, note);
    }
  
}
