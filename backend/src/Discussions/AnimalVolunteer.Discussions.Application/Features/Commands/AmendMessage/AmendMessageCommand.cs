using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.AmendMessage;

public record AmendMessageCommand(string NewText, Guid MessageId, Guid UserId, Guid DiscussionId) : ICommand;

