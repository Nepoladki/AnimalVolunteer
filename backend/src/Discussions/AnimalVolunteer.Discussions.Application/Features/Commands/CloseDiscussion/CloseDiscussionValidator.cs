using FluentValidation;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CloseDiscussion;
public class CloseDiscussionValidator : AbstractValidator<CloseDiscussionCommand>
{
    public CloseDiscussionValidator()
    {
        RuleFor(x => x.DiscussionId).NotEmpty();
    }
}
