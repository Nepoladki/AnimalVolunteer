using AnimalVolunteer.Application.DTOs.Volunteer;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Application.Interfaces;

public interface IReadDbContext
{
    DbSet<VolunteerDto> Volunteers { get; }
    DbSet<PetDto> Pets { get; }
}
