using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.SendRequestForRevision;
public class SendRequestForRevisionValidator : AbstractValidator<SendRequestForRevisionCommand>
{
    public SendRequestForRevisionValidator()
    {
        RuleFor(x => x.RequestId).NotEmpty();
        RuleFor(x => x.RejectionComment).NotEmpty();
    }
}
