using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;

public record GetFilteredVolunteersWithPaginationQuery(
    string? Name,
    int Page, 
    int PageSize) : IQuery;
