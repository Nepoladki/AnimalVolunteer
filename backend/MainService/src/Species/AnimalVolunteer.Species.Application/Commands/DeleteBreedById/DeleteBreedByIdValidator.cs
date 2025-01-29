using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using FluentValidation;

namespace AnimalVolunteer.Species.Application.Commands.DeleteBreedById;

public class DeleteSpeciesByIdValidator : AbstractValidator<DeleteBreedByIdCommand>
{
    public DeleteSpeciesByIdValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.InvalidValue(nameof(SpeciesId)));
        RuleFor(x => x.BreedId).NotEmpty().WithError(Errors.General.InvalidValue(nameof(BreedId)));
    }
}
