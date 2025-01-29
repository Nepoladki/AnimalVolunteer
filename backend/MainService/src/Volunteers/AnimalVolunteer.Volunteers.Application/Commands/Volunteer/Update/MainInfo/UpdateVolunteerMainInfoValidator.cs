using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.MainInfo;

public class UpdateVolunteerMainInfoValidator :
    AbstractValidator<UpdateVolunteerMainInfoCommand>
{
    public UpdateVolunteerMainInfoValidator()
    {
        RuleFor(c => c.FullName)
            .MustBeValueObject(c => FullName.Create(
                c.FirstName,
                c.Patronymic,
                c.LastName));

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
    }
};
