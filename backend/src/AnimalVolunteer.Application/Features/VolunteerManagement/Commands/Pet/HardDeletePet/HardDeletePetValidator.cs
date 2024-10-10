using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.HardDeletePet;
public class HardDeletePetValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty();

        RuleFor(c => c.PetId).NotEmpty();
    }
}
