using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Discussions.Application.Features.Queries.GetDiscussionById;
public record GetDiscussionByRelatedIdCommand(Guid RelatedId) : ICommand;
