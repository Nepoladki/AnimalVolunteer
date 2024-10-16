using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Queries.Volunteer.GetVolunteersWithPagination;

public class GetFilteredVolunteersWithPaginationValidator : AbstractValidator<GetFilteredVolunteersWithPaginationQuery>
{
    public GetFilteredVolunteersWithPaginationValidator()
    {
        RuleFor(q => q.Page).GreaterThanOrEqualTo(1).WithError(Errors.General.InvalidValue("Page"));

        RuleFor(q => q.PageSize).GreaterThanOrEqualTo(1).WithError(Errors.General.InvalidValue("PageSize"));
    }
}
