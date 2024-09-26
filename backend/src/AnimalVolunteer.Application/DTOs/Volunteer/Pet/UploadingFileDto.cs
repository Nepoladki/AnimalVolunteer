using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Application.DTOs.Volunteer.Pet;

public record UploadingFileDto(
    string ObjectName,
    FilePath FilePath,
    Stream Content);
