namespace AnimalVolunteer.Application.DTOs.SpeciesManagement;

public class BreedDto 
{
    public Guid Id { get; init; } 
    public Guid SpeciesId { get; init; }
    public string Breed { get; init; } = string.Empty;
}