using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.ApproveRequest;
public class ApproveRequestValidator : AbstractValidator<ApproveRequestCommand>
{
    public ApproveRequestValidator()
    {
        RuleFor(x => x.RequestId).NotEmpty();
    }
}
