using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;

public record PhysicalParameters
{
    public const string DB_COLUMN_COLOR = "color";
    public const string DB_COLUMN_WEIGHT = "weight";
    public const string DB_COLUMN_HEIGHT = "height";
    public const int MAX_COLOR_LENGTH = 100;
    private const float MIN_WEIGHT_HEIGHT = 0;
    private const float MAX_WEIGHT_HEIGHT = 150;
    private PhysicalParameters(string color, double weight, double height)
    {
        Color = color;
        Weight = weight;
        Height = height;
    }
    public string Color { get; } = default!;
    public double Weight { get; } = default!;
    public double Height { get; } = default!;
    public static Result<PhysicalParameters, Error> Create(string color, double weight, double height)
    {
        if (string.IsNullOrWhiteSpace(color) || color.Length > MAX_COLOR_LENGTH)
            return Errors.General.InvalidValue(nameof(color));

        if (weight < MIN_WEIGHT_HEIGHT || weight > MAX_WEIGHT_HEIGHT)
            return Errors.General.InvalidValue(nameof(weight));

        if (height < MIN_WEIGHT_HEIGHT || height > MAX_WEIGHT_HEIGHT)
            return Errors.General.InvalidValue(nameof(height));

        return new PhysicalParameters(color, weight, height);
    }
}