using FluentValidation;

namespace AnimalVolunteer.Species.Application.Commands.DeleteSpeciesById;

public class DeleteSpeciesByIdValidator : AbstractValidator<DeleteSpeciesByIdCommand>
{
    public DeleteSpeciesByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
