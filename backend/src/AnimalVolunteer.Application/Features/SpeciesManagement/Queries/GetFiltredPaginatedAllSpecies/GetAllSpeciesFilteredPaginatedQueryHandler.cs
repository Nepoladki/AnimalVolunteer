using AnimalVolunteer.Application.DTOs.SpeciesManagement;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Application.Models;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Queries.GetAllSpecies;

public class GetAllSpeciesFilteredPaginatedQueryHandler :
    IQueryHandler<PagedList<SpeciesDto>, GetAllSpeciesFIilteredPaginatedQuery>
{
    private readonly IReadDbContext _readDbContext;
    public GetAllSpeciesFilteredPaginatedQueryHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PagedList<SpeciesDto>> Handle(
        GetAllSpeciesFIilteredPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var dbQuery = _readDbContext.Species;

        dbQuery.WhereIf(
            string.IsNullOrWhiteSpace(query.Name),
            s => s.Name.Contains(query.Name!));

        return await dbQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
