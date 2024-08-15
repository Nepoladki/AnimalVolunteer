using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace AnimalVolunteer.Domain.ValueObjects;

public record PaymentDetails
{
    // EF Core ctor
    private PaymentDetails() { }
    private PaymentDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; } = null!;
    public string Description { get; } = null!;
    public static Result<PaymentDetails> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<PaymentDetails>("Invalid name");

        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.TEXT_LENGTH_LIMIT_MEDIUM)
            return Result.Failure<PaymentDetails>("Invalid description");

        return Result.Success(new PaymentDetails(name, description));
    }
}