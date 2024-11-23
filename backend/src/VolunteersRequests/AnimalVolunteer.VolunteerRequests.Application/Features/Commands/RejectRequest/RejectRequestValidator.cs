using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.RejectRequest;
public class RejectRequestValidator : AbstractValidator<RejectRequestCommand>
{
    public RejectRequestValidator()
    {
        RuleFor(x => x.RequestId).NotEmpty();
        RuleFor(x => x.RejectionComment).NotEmpty();
    }
}
