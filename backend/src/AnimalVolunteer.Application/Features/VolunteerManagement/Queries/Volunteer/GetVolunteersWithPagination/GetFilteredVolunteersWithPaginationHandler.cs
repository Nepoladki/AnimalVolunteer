using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Application.Models;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Volunteer.GetVolunteersWithPagination;
public class GetFilteredVolunteersWithPaginationHandler
    : IQueryHandler<PagedList<VolunteerDto>, GetFilteredVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    public GetFilteredVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetFilteredVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        volunteersQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            v => v.FirstName.Contains(query.Name!));

        return await volunteersQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
