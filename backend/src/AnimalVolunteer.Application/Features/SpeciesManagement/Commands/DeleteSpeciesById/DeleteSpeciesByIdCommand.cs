using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Commands.DeleteSpeciesById;

public record DeleteSpeciesByIdCommand(
    Guid Id) : ICommand;
