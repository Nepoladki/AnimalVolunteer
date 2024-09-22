namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;

public record PetPhotoList
{
    private PetPhotoList() { }
    private PetPhotoList(IEnumerable<PetPhoto> photos) => PetPhotos = photos.ToList();
    public IReadOnlyList<PetPhoto> PetPhotos { get; } = null!;
    public static PetPhotoList Create(IEnumerable<PetPhoto> photos) => new(photos);
}