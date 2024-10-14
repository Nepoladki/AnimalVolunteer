using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetById;

public class GetPetByIdValidator : AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdValidator()
    {
        RuleFor(q => q.Id).NotEmpty();
    }
}
