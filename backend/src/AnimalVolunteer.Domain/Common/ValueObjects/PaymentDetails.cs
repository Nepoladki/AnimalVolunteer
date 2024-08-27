using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Common.ValueObjects;

public record PaymentDetails
{
    public const int MAX_NAME_LENGTH = 50;
    public const int MAX_DESC_LENGTH = 500;
    private PaymentDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; } = null!;
    public string Description { get; } = null!;
    public static Result<PaymentDetails, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(name));

        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESC_LENGTH)
            return Errors.General.InvalidValue(nameof(description));

        return new PaymentDetails(name, description);
    }
}