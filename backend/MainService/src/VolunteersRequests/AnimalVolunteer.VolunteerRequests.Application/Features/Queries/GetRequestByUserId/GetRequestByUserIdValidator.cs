using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestByUserId;

public class GetRequestByUserIdValidator : AbstractValidator<GetRequestByUserIdQuery>
{
    public GetRequestByUserIdValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}

