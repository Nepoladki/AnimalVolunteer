using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Commands.DeleteBreedById;

public record DeleteBreedByIdCommand(
    Guid SpeciesId,
    Guid BreedId) : ICommand;
