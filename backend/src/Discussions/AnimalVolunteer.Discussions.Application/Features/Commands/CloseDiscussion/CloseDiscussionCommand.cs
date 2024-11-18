using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CloseDiscussion;
public record CloseDiscussionCommand(Guid RelatedId) : ICommand;
