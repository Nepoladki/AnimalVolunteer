using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
}
