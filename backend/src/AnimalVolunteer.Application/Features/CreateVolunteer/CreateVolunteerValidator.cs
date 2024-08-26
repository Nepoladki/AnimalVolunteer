using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.CreateVolunteer;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleFor(c => new { c.FirstName, c.SurName, c.LastName })
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.SurName, x.LastName));
    }
}
