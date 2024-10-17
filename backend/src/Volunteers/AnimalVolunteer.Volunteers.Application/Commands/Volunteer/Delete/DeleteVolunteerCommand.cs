using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Delete;

public record DeleteVolunteerCommand(Guid Id) : ICommand;
