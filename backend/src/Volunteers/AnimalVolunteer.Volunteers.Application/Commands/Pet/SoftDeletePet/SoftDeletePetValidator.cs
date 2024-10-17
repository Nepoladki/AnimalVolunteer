using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.SoftDeletePet;

public class SoftDeletePetValidator : AbstractValidator<SoftDeletePetCommand>
{
    public SoftDeletePetValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty();

        RuleFor(c => c.PetId).NotEmpty();
    }
}
