using AnimalVolunteer.Volunteers.Application.Commands.Pet.ChangePetStatus;
using AnimalVolunteer.Volunteers.Domain.Enums;

namespace AnimalVolunteer.Volunteers.Web.Requests.Pet;

public record ChangePetStatusRequest(CurrentStatus NewStatus)
{
    public ChangePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, NewStatus);
}