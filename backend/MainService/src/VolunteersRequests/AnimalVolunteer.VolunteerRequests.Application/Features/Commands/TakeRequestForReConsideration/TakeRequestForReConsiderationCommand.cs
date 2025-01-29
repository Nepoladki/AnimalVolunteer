using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForReConsideration;
public record TakeRequestForReConsiderationCommand(Guid RequestId) : ICommand;
