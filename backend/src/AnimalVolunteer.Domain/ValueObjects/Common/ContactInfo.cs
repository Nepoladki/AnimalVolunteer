using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.ValueObjects.Common;

public record ContactInfo
{
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
        if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Errors.General.InvalidValue(nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Errors.General.InvalidValue(nameof(name));

        if (note is not null && note.Length > Constants.TEXT_LENGTH_LIMIT_MEDIUM)
            return Errors.General.InvalidValue(nameof(note));

        return new ContactInfo(phoneNumber, name, note);
    }
}
