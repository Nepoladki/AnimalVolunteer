using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Aggregates.SpeciesManagement.Root;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Commands.DeleteSpeciesById;

public class DeleteSpeciesByIdHandler : 
    ICommandHandler<DeleteSpeciesByIdCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesByIdHandler> _logger;
    private readonly IValidator<DeleteSpeciesByIdCommand> _validator;

    public DeleteSpeciesByIdHandler(
        ISpeciesRepository speciesRepository,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesByIdHandler> logger,
        IValidator<DeleteSpeciesByIdCommand> validator)
    {
        _speciesRepository = speciesRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeleteSpeciesByIdCommand command, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

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