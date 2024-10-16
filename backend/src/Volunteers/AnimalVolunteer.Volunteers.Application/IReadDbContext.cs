using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.Core.DTOs.Volunteers.Pet;

namespace AnimalVolunteer.Volunteers.Application;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
}
