using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.SharedKernel;
using FluentValidation;
using AnimalVolunteer.Core.Validation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.SoftDeletePet;

public class SoftDeletePetValidator : AbstractValidator<SoftDeletePetCommand>
{
    public SoftDeletePetValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty()
            .WithError(Errors.General.InvalidValue(nameof(VolunteerId)));
        
        RuleFor(c => c.PetId).NotEmpty()
            .WithError(Errors.General.InvalidValue(nameof(PetId)));
    }
}
