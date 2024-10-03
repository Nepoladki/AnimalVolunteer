using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record GetFilteredVolunteersWithPaginationRequest(
    string? Name,
    int Page, 
    int PageSize)
{
    public GetFilteredVolunteersWithPaginationQuery ToQuery() =>
        new(Name, Page, PageSize);
}
