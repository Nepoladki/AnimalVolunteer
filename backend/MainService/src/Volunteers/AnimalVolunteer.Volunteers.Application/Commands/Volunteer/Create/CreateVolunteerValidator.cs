using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Create;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleFor(c => c.FullName)
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.Patronymic, x.LastName));

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
    }
}
