using FluentValidation;

namespace AnimalVolunteer.Species.Application.Commands.DeleteBreedById;

public class DeleteSpeciesByIdValidator : AbstractValidator<DeleteBreedByIdCommand>
{
    public DeleteSpeciesByIdValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty();
        RuleFor(x => x.BreedId).NotEmpty();
    }
}
