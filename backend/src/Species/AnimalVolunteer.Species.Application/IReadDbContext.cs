using AnimalVolunteer.Core.DTOs.Species;

namespace AnimalVolunteer.Species.Application;

public interface IReadDbContext
{
    IQueryable<SpeciesDto> Species { get; }
    IQueryable<BreedDto> Breeds { get; }
}
