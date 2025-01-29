using AnimalVolunteer.VolunteerRequests.Domain.Enums;
using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestsByAdminId;

public class GetRequestsByAdminIdValidator : AbstractValidator<GetRequestsByAdminIdQuery>
{
    public GetRequestsByAdminIdValidator()
    {
        RuleFor(x => x.AdminId).NotEmpty();
        RuleFor(x => x.Page).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty();
        RuleFor(x => x.Status).IsInEnum().Unless(x => x.Status is null);
    }
}

