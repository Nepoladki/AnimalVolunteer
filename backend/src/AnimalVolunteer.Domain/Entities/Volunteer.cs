namespace AnimalVolunteer.Domain.Entities;

public class Volunteer
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int ExpirienceYears { get; private set; }
    public int PetsFoundedHome { get; private set; }
    public int PetsLookingForHome { get; private set; }
    public int PetsInVetClinic { get; private set; }
    public string PhoneNumber { get; private set; } = null!;
    public List<SocialNetwork> SocialNetworks { get; private set; } = [];
    public List<PaymentDetails> PaymentDetails { get; private set; } = [];
    public List<Pet> Pets { get; private set; } = [];
}
