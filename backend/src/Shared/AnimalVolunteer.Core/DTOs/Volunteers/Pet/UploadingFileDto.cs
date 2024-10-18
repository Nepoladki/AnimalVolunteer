using AnimalVolunteer.SharedKernel.ValueObjects;

namespace AnimalVolunteer.Core.DTOs.Volunteers.Pet;

public record UploadingFileDto(
    FilePath FilePath,
    Stream Content);
