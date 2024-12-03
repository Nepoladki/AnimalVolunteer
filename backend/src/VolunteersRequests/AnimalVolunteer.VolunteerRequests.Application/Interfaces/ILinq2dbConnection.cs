using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using LinqToDB;

namespace AnimalVolunteer.VolunteerRequests.Application.Interfaces;
public interface ILinq2dbConnection
{
    ITable<VolunteerRequestDto> VolunteerRequests { get; }
}
