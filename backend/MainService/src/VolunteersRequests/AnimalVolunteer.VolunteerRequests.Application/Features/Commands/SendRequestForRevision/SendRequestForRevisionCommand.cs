
using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.SendRequestForRevision;
public record SendRequestForRevisionCommand(Guid RequestId, string RejectionComment) : ICommand;
