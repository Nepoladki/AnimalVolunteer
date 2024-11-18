using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.AddMessage;
public record AddMessageCommand(string Text, Guid UserId, Guid RelatedId) : ICommand;
