using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestsForConsideration;

public class GetRequestsForConsiderationValidator : AbstractValidator<GetRequestsForConsiderationQuery>
{
    public GetRequestsForConsiderationValidator()
    {
        RuleFor(x => x.Page).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty();
    }
}

