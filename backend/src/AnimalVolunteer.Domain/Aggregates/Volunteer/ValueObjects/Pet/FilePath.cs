using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;

public record FilePath
 {
    public const int MAX_FILEPATH_LENGTH = 200;
    public string Path { get; }
    private FilePath(string path)
    {
        Path = path;
    }
    public static Result<FilePath, Error> Create(Guid path, string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return Errors.General.InvalidValue(nameof(extension));

        if (path == Guid.Empty)
            return Errors.General.InvalidValue(nameof(path));

        // Other Validation

        var fullPath = path + extension;

        return new FilePath(fullPath); 
    }
    public static Result<FilePath, Error> Create(string fullPath)
    {
        return new FilePath(fullPath);
    }
 }
