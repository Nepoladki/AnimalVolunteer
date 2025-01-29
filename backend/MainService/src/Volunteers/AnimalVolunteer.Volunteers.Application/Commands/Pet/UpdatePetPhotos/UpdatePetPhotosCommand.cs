using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Volunteers.Pet;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePetPhotos;

public record UpdatePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files) : ICommand;


