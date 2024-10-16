namespace AnimalVolunteer.Core.DTOs.Volunteers.Pet;

public record UploadFileDto(
    string FileName,
    Stream Content);
