using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.DeleteMessage;

public record DeleteMessageCommand(Guid DiscussionId, Guid MessageId, Guid UserId) : ICommand;

