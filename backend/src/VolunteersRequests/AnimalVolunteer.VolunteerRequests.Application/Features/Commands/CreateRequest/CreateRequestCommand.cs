using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.VolunteerRequests;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.CreateRequest;
public record CreateRequestCommand(Guid UserId, VolunteerInfoDto VolunteerInfo) : ICommand;
