using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Volunteers.Contracts;
using AnimalVolunteer.Volunteers.Contracts.Requests;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;

namespace AnimalVolunteer.Species.Application.Commands.DeleteBreedById;

public class DeleteBreedByIdHandler :
    ICommandHandler<DeleteBreedByIdCommand>
{
    private readonly ILogger<DeleteBreedByIdHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteBreedByIdCommand> _validator;
    private readonly IVolunteersContract _volunteersContract;

    public DeleteBreedByIdHandler(
        ILogger<DeleteBreedByIdHandler> logger,
        ISpeciesRepository speciesRepository,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork,
        IValidator<DeleteBreedByIdCommand> validator,
        IVolunteersContract volunteersContract)
    {
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersContract = volunteersContract;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        DeleteBreedByIdCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var anyPet = await _volunteersContract.AnyPetExistsByBreed(
            new AnyPetExistsByBreedRequest(command.BreedId), cancellationToken);
        if (anyPet == true)
            return Errors.General.DeleteingConflict(command.BreedId).ToErrorList();

        var species = await _speciesRepository.GetSpeciesById(
            SpeciesId.CreateWithGuid(command.SpeciesId), cancellationToken);
        if (species is null)
            return Errors.General.NotFound(command.SpeciesId).ToErrorList();

        var breedToDelete = species.Breeds
            .FirstOrDefault(b => (Guid)b.Id == command.BreedId);
        if (breedToDelete is null)
            return Errors.General.NotFound(command.BreedId).ToErrorList();

        var deletingResult = species.DeleteBreed(breedToDelete);
        if (deletingResult.IsFailure)
            return deletingResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Deleted Breed with name {name} and id {id}",
            breedToDelete.Name,
            breedToDelete.Id);

        return Result.Success<ErrorList>();
    }
}
