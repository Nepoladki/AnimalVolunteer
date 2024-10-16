using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Core.DTOs.Volunteers.Pet;

public record UploadingFileDto(
    FilePath FilePath,
    Stream Content);
