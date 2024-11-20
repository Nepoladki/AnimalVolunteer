using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestOnConsideration;
public class TakeRequestForConsiderationValidator : AbstractValidator<TakeRequestForConsiderationCommand>
{
    public TakeRequestForConsiderationValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.AdminId).NotEmpty();
    }
}
