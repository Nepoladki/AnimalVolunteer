namespace AnimalVolunteer.Application.DTOs.Volunteer.Pet;

public record UploadFileDto(
    string FileName,
    Stream Content);
