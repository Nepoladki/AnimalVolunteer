namespace AnimalVolunteer.Application.DTOs.Volunteer.Pet;

public record FileToUploadDto(
    string ObjectName,
    Stream Content);
