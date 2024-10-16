using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.HardDeletePet;

public record HardDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
