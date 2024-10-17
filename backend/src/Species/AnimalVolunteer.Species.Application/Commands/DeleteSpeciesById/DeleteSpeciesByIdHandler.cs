using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Volunteers.Contracts;
using AnimalVolunteer.Volunteers.Contracts.Requests;

namespace AnimalVolunteer.Species.Application.Commands.DeleteSpeciesById;

public class DeleteSpeciesByIdHandler :
    ICommandHandler<DeleteSpeciesByIdCommand>
{
    private readonly IVolunteersContract _volunteersContract;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesByIdHandler> _logger;
    private readonly IValidator<DeleteSpeciesByIdCommand> _validator;

    public DeleteSpeciesByIdHandler(
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesByIdHandler> logger,
        IValidator<DeleteSpeciesByIdCommand> validator,
        IVolunteersContract volunteersContract)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
        _volunteersContract = volunteersContract;
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

        var anyPet = await _volunteersContract.AnyPetExistsBySpecies(
            new AnyPetExistsBySpeciesRequest(speciesId), cancellationToken);
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