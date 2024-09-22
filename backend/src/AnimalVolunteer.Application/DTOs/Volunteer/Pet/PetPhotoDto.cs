namespace AnimalVolunteer.Application.DTOs.Volunteer.Pet;

public record PetPhotoDto(
    string FileName,
    Stream Content,
    bool IsMain);
