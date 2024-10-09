using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.ChangePetStatus;

public class ChangePetStatusValidator : AbstractValidator<ChangePetStatusCommand>
{
    public ChangePetStatusValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty();
        
        RuleFor(c => c.PetId).NotEmpty();

        RuleFor(c => c.NewStatus).IsInEnum();
    }
}
