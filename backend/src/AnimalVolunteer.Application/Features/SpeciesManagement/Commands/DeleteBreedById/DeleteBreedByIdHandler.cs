using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
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
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBreedByIdHandler(
        ILogger<DeleteBreedByIdHandler> logger,
        IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _readDbContext = readDbContext;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        DeleteBreedByIdCommand command, 
        CancellationToken cancellationToken)
    {
        var anyPet = await _readDbContext.Pets
            .AnyAsync(p => p.BreedId == command.BreedId, cancellationToken);
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
