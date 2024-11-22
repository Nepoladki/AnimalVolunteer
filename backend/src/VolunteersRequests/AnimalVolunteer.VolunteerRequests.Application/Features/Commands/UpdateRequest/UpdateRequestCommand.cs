using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.VolunteerRequests;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequest;
public record UpdateRequestCommand(Guid RequestId, VolunteerInfoDto VolunteerInfo) : ICommand;
