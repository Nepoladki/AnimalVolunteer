using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CreateDiscussion;
public record CreateDiscussionCommand(Guid RelatedId, Guid UserId, Guid AdminId) : ICommand;
