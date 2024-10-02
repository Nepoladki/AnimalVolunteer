using AnimalVolunteer.Application.DTOs.Volunteer.Pet;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files);


