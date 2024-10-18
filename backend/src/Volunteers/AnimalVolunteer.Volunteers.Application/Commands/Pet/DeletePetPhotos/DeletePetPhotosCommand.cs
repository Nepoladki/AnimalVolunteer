using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.DeletePetPhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId) : ICommand;