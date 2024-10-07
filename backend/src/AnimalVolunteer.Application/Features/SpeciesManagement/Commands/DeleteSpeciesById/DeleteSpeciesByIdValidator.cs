using FluentValidation;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Commands.DeleteSpeciesById;

public class DeleteSpeciesByIdValidator : AbstractValidator<DeleteSpeciesByIdCommand>
{
    public DeleteSpeciesByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
