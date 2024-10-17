using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.GetPetById;

public class GetPetByIdValidator : AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdValidator()
    {
        RuleFor(q => q.Id).NotEmpty();
    }
}
