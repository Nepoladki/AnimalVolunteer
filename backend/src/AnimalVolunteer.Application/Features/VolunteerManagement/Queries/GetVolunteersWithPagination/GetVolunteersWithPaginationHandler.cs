using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;
public class GetVolunteersWithPaginationHandler
{
    private readonly IReadDbContext _readDbContext;
    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var issuesQuery = _readDbContext.Volunteers;

        return await issuesQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
