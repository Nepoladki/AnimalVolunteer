namespace AnimalVolunteer.Domain.ValueObjects.Pet;

public record PetPhotoList
{
    private PetPhotoList () {}
    public List<PetPhoto> PetPhotos { get; private set; } = null!;
}