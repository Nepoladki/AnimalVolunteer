using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects;

namespace AnimalVolunteer.Domain.Entities;

public sealed class Pet : Entity<PetId>
{
    // EF Core ctor
    private Pet(PetId id) : base(id) { }
    public string Name { get; private set; } = null!;
    public string Species { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Breed { get; private set; } = null!;
    public string Color { get; private set; } = null!;
    public HealthInfo HealthInfo { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public float Weight { get; private set; }
    public float Height { get; private set; }
    public ContactInfoList ContactInfos { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; }
    public string CurrentStatus { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public PaymentDetailsList PaymentDetails { get; private set; } = default!;
    public PetPhotoList PetPhotos { get; private set; } = default!;    
}