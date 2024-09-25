using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Features.Volunteer.AddPet;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files);


