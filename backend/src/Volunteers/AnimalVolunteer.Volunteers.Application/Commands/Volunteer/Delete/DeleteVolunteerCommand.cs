using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Delete;

public record DeleteVolunteerCommand(Guid Id) : ICommand;
