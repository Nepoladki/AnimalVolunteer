using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.RejectRequest;
public record RejectRequestCommand(Guid RequestId, string RejectionComment) : ICommand;
