using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForReConsideration;
public class TakeRequestForReConsiderationValidator : AbstractValidator<TakeRequestForReConsiderationCommand>
{
    public TakeRequestForReConsiderationValidator()
    {
        RuleFor(x => x.RequestId).NotEmpty();
    }
}
