using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Volunteer.GetVolunteersWithPagination;

public record GetFilteredVolunteersWithPaginationQuery(
    string? Name,
    int Page,
    int PageSize) : IQuery;
