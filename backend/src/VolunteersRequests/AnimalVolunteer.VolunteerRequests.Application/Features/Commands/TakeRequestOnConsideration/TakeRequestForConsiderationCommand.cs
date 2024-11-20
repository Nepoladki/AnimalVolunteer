using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestOnConsideration;
public record TakeRequestForConsiderationCommand(Guid RequestId, Guid UserId, Guid AdminId) : ICommand;
