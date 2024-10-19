using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.ChangePetStatus;

public class ChangePetStatusValidator : AbstractValidator<ChangePetStatusCommand>
{
    public ChangePetStatusValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty()
            .WithError(Errors.General.InvalidValue(nameof(VolunteerId)));

        RuleFor(c => c.PetId).NotEmpty()
            .WithError(Errors.General.InvalidValue(nameof(PetId)));

        RuleFor(c => c.NewStatus).IsInEnum()
            .WithError(Errors.General.InvalidValue("NewStatus"));
    }
}
