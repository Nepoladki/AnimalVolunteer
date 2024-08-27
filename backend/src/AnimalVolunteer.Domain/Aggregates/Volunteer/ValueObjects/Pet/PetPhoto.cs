using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;

public record PetPhoto
{
    public const int MAX_FILEPATH_LENGTH = 200;
    private PetPhoto(string filePath, bool isMain)
    {
        FilePath = filePath;
        IsMain = isMain;
    }
    public string FilePath { get; } = null!;
    public bool IsMain { get; }
    public static Result<PetPhoto, Error> Create(string filePath, bool isMain)
    {
        if (string.IsNullOrWhiteSpace(filePath) || filePath.Length > MAX_FILEPATH_LENGTH)
            return Errors.General.InvalidValue(nameof(filePath));

        return new PetPhoto(filePath, isMain);
    }
}
