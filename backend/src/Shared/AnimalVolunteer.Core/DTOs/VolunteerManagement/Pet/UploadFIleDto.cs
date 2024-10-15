namespace AnimalVolunteer.Core.DTOs.VolunteerManagement.Pet;

public record UploadFileDto(
    string FileName,
    Stream Content);
