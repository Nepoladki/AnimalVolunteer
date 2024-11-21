using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequestAfterRevision;
public record UpdateRequestAfterRevisionCommand(Guid RequestId) : ICommand;
