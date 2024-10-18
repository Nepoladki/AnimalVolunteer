using AnimalVolunteer.Core.DTOs.Volunteers.Pet;
using AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePetPhotos;
using Microsoft.AspNetCore.Http;

namespace AnimalVolunteer.Volunteers.Web.Requests.Pet;

public record UpdatePetPhotosRequest(
    IFormFileCollection Files)
{
    public UpdatePetPhotosCommand ToCommand(
        Guid volunteerId, Guid petId, IEnumerable<UploadFileDto> files)
    {
        return new(volunteerId, petId, files);
    }
}
