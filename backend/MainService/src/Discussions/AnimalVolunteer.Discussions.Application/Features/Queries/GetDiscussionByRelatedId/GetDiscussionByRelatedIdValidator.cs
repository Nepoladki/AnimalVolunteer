using AnimalVolunteer.Discussions.Application.Features.Queries.GetDiscussionById;
using FluentValidation;

namespace AnimalVolunteer.Discussions.Application.Features.Queries.GetDiscussionByRelatedId;
public class GetDiscussionByRelatedIdValidator : AbstractValidator<GetDiscussionByRelatedIdCommand>
{
    public GetDiscussionByRelatedIdValidator()
    {
        RuleFor(x => x.RelatedId).NotEmpty();
    }
}
