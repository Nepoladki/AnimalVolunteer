using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.CreateRequest;
public class CreateRequestValidator : AbstractValidator<CreateRequestCommand>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.VolunteerInfo).NotNull();
    }
}
