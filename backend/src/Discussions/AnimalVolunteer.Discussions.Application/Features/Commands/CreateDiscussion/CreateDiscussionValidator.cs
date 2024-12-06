using FluentValidation;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CreateDiscussion;
public class CreateDiscussionValidator : AbstractValidator<CreateDiscussionCommand>
{
    public CreateDiscussionValidator()
    {
        RuleFor(x => x.RelatedId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.AdminId).NotEmpty();
    }
}
