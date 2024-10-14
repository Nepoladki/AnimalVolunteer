using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;
