using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.ChangePetStatus;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;

public record ChangePetStatusRequest(CurrentStatus NewStatus)
{
    public ChangePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, NewStatus);
}