using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Species.Application.Commands.DeleteBreedById;

public record DeleteBreedByIdCommand(
    Guid SpeciesId,
    Guid BreedId) : ICommand;
