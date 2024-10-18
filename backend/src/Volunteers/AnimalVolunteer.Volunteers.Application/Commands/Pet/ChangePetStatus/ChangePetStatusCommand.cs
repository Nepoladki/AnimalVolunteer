using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Volunteers.Domain.Enums;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.ChangePetStatus;

public record ChangePetStatusCommand(
    Guid VolunteerId,
    Guid PetId,
    CurrentStatus NewStatus) : ICommand;
