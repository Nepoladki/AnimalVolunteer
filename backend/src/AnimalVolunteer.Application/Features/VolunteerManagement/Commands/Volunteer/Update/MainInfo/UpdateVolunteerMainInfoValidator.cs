using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.MainInfo;

public class UpdateVolunteerMainInfoValidator :
    AbstractValidator<UpdateVolunteerMainInfoCommand>
{
    public UpdateVolunteerMainInfoValidator()
    {
        RuleFor(c => c)
            .MustBeValueObject(c => FullName.Create(
                c.FullName.FirstName,
                c.FullName.SurName,
                c.FullName.LastName));

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
    }
};
