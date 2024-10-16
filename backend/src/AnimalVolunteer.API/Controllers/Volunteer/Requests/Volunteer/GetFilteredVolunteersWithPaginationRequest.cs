﻿using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Volunteer.GetVolunteersWithPagination;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record GetFilteredVolunteersWithPaginationRequest(
    string? Name,
    string? SortBy,
    string? SortDirection,
    int Page, 
    int PageSize)
{
    public GetFilteredVolunteersWithPaginationQuery ToQuery() =>
        new(Name, SortBy, SortDirection, Page, PageSize);
}
