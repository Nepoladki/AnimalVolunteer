using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;

public record HealthInfo
{
    public const int MAX_DESC_LENGTH = 2000;
    public const string DB_COLUMN_NAME = "health_description";
    private HealthInfo(string description, bool isVaccinated, bool isNeutered)
    {
        Description = description;
        IsVaccinated = isVaccinated;
        IsNeutered = isNeutered;
    }
    public string Description { get; } = null!;
    public bool IsVaccinated { get; }
    public bool IsNeutered { get; }
    public static Result<HealthInfo, Error> Create(string description, bool isVaccinated, bool isNeutered)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESC_LENGTH)
            return Errors.General.InvalidValue(nameof(description));

        return new HealthInfo(description, isVaccinated, isNeutered);
    }
}
