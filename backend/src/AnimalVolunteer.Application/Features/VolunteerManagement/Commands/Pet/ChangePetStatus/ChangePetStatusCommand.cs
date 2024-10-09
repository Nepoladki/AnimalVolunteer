using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.ChangePetStatus;

public record ChangePetStatusCommand(
    Guid VolunteerId, 
    Guid PetId,
    CurrentStatus NewStatus) : ICommand;
