using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Core.DTOs.VolunteerManagement.Pet;

public record UploadingFileDto(
    FilePath FilePath,
    Stream Content);
