using System.Security.Cryptography.X509Certificates;

namespace AnimalVolunteer.API.Contracts;

public record AddPetPhotosRequest(
    IFormFileCollection Files);
