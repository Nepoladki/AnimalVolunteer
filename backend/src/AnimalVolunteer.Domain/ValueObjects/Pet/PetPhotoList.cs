namespace AnimalVolunteer.Domain.ValueObjects.Pet;

public record PetPhotoList
{
    public List<PetPhoto> PetPhotos { get; private set; } = null!;
}