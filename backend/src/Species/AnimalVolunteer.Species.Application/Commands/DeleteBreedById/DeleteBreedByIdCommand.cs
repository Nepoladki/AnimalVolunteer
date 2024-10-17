using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Species.Application.Commands.DeleteBreedById;

public record DeleteBreedByIdCommand(
    Guid SpeciesId,
    Guid BreedId) : ICommand;
