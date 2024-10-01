using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;

public record AddPetPhotosRequest(
    IFormFileCollection Files)
{
    public AddPetPhotosCommand ToCommand(
        Guid volunteerId, Guid petId, IEnumerable<UploadFileDto> files)
    {
        return new(volunteerId, petId, files);
    }
}
