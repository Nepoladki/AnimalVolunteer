using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.AddPetPhotos;

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
