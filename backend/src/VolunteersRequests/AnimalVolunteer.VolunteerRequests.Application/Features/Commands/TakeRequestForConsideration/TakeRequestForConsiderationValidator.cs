using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForConsideration;
public class TakeRequestForConsiderationValidator : AbstractValidator<TakeRequestForConsiderationCommand>
{
    public TakeRequestForConsiderationValidator()
    {
        RuleFor(x => x.RequestId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.AdminId).NotEmpty();
    }
}
