using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Pet;

public record PetPhoto
{
    public const string DB_COLUMN_NAME = "pet_photos";
    private PetPhoto(FilePath filePath, bool isMain)
    {
        FilePath = filePath;
        IsMain = isMain;
    }
    public FilePath FilePath { get; } = null!;
    public bool IsMain { get; }
    public static Result<PetPhoto, Error> Create(FilePath filePath, bool isMain)
    {
        return new PetPhoto(filePath, isMain);
    }
}
