using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Species.Application.Commands.DeleteSpeciesById;

public record DeleteSpeciesByIdCommand(
    Guid Id) : ICommand;
