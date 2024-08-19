using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using System.Reflection.Metadata;

namespace AnimalVolunteer.Domain.ValueObjects.Pet;

public record PetPhoto
{
    private PetPhoto(string filePath, bool isMain)
    {
        FilePath = filePath;
        IsMain = isMain;
    }
    public string FilePath { get; } = null!;
    public bool IsMain { get; }
    public static Result<PetPhoto> Create(string filePath, bool isMain)
    {
        if (string.IsNullOrWhiteSpace(filePath) || filePath.Length > Constants.TEXT_LENGTH_LIMIT_MEDIUM)
            return Result.Failure<PetPhoto>("Invalid file path");

        return Result.Success(new PetPhoto(filePath, isMain));
    }
}
