using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Commands.DeleteBreedById;

public class DeleteBreedByIdHandler : 
    ICommandHandler<DeleteBreedByIdCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<DeleteBreedByIdHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    public DeleteBreedByIdHandler(
        ILogger<DeleteBreedByIdHandler> logger,
        IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository)
    {
        _logger = logger;
        _readDbContext = readDbContext;
        _speciesRepository = speciesRepository;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        DeleteBreedByIdCommand command, 
        CancellationToken cancellationToken)
    {
        var anyPet = await _readDbContext.Pets
            .AnyAsync(p => p.BreedId == command.BreedId, cancellationToken);
        if (anyPet == true)
            return Errors.General.DeleteingConflict(command.BreedId).ToErrorList();

        _speciesRepository.DeleteBreed(SpeciesIdcommand.SpeciesId)
    }
}
