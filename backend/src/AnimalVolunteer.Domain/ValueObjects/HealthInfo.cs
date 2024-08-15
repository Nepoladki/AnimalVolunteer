using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.ValueObjects;

public record HealthInfo
{
    // EF Core ctor
    private HealthInfo() { }
    private HealthInfo(string description, bool isVaccinated, bool isNeutered)
    {
        Description = description;
        IsVaccinated = isVaccinated;
        IsNeutered = isNeutered;
    }
    public string Description { get; } = null!;
    public bool IsVaccinated { get; }
    public bool IsNeutered { get; }
    public static Result<HealthInfo> Create(string description, bool isVaccinated, bool isNeutered)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.TEXT_LENGTH_LIMIT_HIGH)
            return Result.Failure<HealthInfo>("Invalid description");

        return Result.Success(new HealthInfo(description, isVaccinated, isNeutered));
    }
}
