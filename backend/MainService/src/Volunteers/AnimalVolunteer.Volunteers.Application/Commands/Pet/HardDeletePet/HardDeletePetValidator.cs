using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.HardDeletePet;
public class HardDeletePetValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty()
            .WithError(Errors.General.InvalidValue(nameof(VolunteerId)));

        RuleFor(c => c.PetId).NotEmpty()
            .WithError(Errors.General.InvalidValue(nameof(PetId)));
    }
}
