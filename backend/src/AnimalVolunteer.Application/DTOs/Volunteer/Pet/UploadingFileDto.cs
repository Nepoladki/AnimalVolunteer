using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Application.DTOs.Volunteer.Pet;

public record UploadingFileDto(
    FilePath FilePath,
    Stream Content);
