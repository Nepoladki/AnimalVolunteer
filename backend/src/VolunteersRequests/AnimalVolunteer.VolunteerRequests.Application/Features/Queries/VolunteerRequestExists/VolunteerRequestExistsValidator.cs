using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.VolunteerRequestExists;

public class VolunteerRequestExistsValidator : AbstractValidator<VolunteerRequestExistsQuery>
{
    public VolunteerRequestExistsValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}

