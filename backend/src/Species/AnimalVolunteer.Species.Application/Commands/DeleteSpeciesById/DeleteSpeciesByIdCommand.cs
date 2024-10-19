using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Species.Application.Commands.DeleteSpeciesById;

public record DeleteSpeciesByIdCommand(
    Guid Id) : ICommand;
