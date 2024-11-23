using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.ApproveRequest;
public record ApproveRequestCommand(Guid RequestId) : ICommand;
