using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Delete;

public record DeleteVolunteerCommand(Guid Id) : ICommand;
