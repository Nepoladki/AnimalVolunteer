using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Queries.Volunteer.GetVolunteersWithPagination;

public record GetFilteredVolunteersWithPaginationQuery(
    string? Name,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;
