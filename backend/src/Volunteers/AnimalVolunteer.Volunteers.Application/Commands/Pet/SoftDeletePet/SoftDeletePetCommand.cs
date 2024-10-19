using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.SoftDeletePet;

public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
