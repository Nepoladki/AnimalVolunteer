namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;

public record AddPetPhotosRequest(
    IFormFileCollection Files);
