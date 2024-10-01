using AnimalVolunteer.Application.DTOs.Volunteer.Pet;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files);


