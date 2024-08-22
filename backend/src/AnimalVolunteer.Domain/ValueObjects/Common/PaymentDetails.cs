using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace AnimalVolunteer.Domain.ValueObjects.Common;

public record PaymentDetails
{
    private PaymentDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; } = null!;
    public string Description { get; } = null!;
    public static Result<PaymentDetails, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Errors.General.InvalidValue(nameof(name));

        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.TEXT_LENGTH_LIMIT_MEDIUM)
            return Errors.General.InvalidValue(nameof(description));

        return new PaymentDetails(name, description);
    }
}