using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Common;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetsFilteredPaginated;

public class GetsPetsFilteredPaginatedValidator : AbstractValidator<GetPetsFilteredPaginatedQuery>
{
    public GetsPetsFilteredPaginatedValidator()
    {
        RuleFor(q => q.Page).GreaterThanOrEqualTo(1).WithError(Errors.General.InvalidValue("Page"));

        RuleFor(q => q.PageSize).GreaterThanOrEqualTo(1).WithError(Errors.General.InvalidValue("PageSize"));
    }
}
