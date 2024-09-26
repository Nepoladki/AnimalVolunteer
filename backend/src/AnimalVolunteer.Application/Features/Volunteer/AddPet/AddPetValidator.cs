using FluentValidation;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPet
{
    public class AddPetValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
