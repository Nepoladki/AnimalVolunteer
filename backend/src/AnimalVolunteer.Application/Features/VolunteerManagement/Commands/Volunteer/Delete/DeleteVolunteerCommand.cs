using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Delete;

public record DeleteVolunteerCommand(Guid Id) : ICommand;
