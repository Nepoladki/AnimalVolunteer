using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.CreateRequest;
public record CreateRequestCommand(Guid UserId) : ICommand;
