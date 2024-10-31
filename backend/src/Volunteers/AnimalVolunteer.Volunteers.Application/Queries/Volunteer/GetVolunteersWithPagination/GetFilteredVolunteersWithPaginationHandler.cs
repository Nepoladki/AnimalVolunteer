using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Models;
using AnimalVolunteer.Volunteers.Application.Interfaces;
using System.Linq.Expressions;

namespace AnimalVolunteer.Volunteers.Application.Queries.Volunteer.GetVolunteersWithPagination;
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

        var keySelector = SortByProperty(query.SortBy);

        volunteersQuery = query.SortDirection?.ToLower() == "desc"
            ? volunteersQuery.OrderByDescending(keySelector)
            : volunteersQuery.OrderBy(keySelector);

        volunteersQuery = volunteersQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            v => v.FirstName.Contains(query.Name!));

        return await volunteersQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
    private static Expression<Func<VolunteerDto, object>> SortByProperty(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return volunteer => volunteer.Id;

        Expression<Func<VolunteerDto, object>> keySelector = sortBy?.ToLower() switch
        {
            "firstname" => v => v.FirstName,
            "surname" => v => v.Surname,
            "lastname" => v => v.LastName,
            _ => v => v.Id
        };

        return keySelector;
    }
}
