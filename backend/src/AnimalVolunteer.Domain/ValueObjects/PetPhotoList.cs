namespace AnimalVolunteer.Domain.ValueObjects;

public record PetPhotoList
{
    public List<PetPhoto> PetPhotos { get; private set; } = null!;
}