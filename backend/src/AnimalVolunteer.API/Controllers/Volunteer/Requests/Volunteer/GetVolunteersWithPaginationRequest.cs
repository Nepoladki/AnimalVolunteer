using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;

public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}
