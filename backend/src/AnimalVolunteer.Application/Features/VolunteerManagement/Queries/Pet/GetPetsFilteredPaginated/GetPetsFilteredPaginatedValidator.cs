using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetsFilteredPaginated;

public class GetPetsFilteredPaginatedValidator : AbstractValidator<GetPetsFilteredPaginatedQuery>
{
    public GetPetsFilteredPaginatedValidator()
    {
        RuleFor(q => q.VolunteerId).NotEmpty();
    }
}
