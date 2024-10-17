using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.ChangePetStatus;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;

namespace AnimalVolunteer.Volunteers.Web.Requests.Pet;

public record ChangePetStatusRequest(CurrentStatus NewStatus)
{
    public ChangePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, NewStatus);
}