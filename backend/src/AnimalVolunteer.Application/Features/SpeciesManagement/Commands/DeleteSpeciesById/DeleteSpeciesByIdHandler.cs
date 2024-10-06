using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.PetType;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Commands.DeleteSpeciesById;

public class DeleteSpeciesByIdHandler : 
    ICommandHandler<DeleteSpeciesByIdCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesByIdHandler> _logger;

    public DeleteSpeciesByIdHandler(
        ISpeciesRepository speciesRepository,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesByIdHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeleteSpeciesByIdCommand command, 
        CancellationToken cancellationToken)
    {
        var speciesId = SpeciesId.CreateWithGuid(command.Id);

        var speciesToDelete = await _speciesRepository
            .GetSpeciesById(speciesId, cancellationToken);
        if (speciesToDelete is null)
            return Errors.General.NotFound(speciesId.Value).ToErrorList();

        var anyPet = _readDbContext.Pets
            .Any(p => p.SpeciesId == speciesId.Value);
        if (anyPet == true)
            return Errors.General
                .DeleteingConflict(speciesId.Value, nameof(Species)).ToErrorList();
        
        _speciesRepository.DeleteSpecies(speciesToDelete);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Deleted species with name {name} and id {id}",
            speciesToDelete.Name,
            speciesToDelete.Id);

        return Result.Success<ErrorList>();
    }
}