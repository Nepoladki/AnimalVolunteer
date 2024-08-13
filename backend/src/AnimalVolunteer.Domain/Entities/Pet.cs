using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalVolunteer.Domain.Entities;

public class Pet
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Species { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Breed { get; private set; } = null!;
    public string Color { get; private set; } = null!;
    public string HealthInfo { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public float Weight { get; private set; }
    public float Height { get; private set; }
    public string PhoneNumber { get; private set; } = null!;
    public bool IsNeutered { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; }
    public string CurrentStatus { get; private set; } = null!;
    public List<PaymentDetails> PaymentsDetails { get; private set; } = default!;
    public List<PetPhoto> PetPhotos { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
}
