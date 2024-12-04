using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Species;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Models;
using AnimalVolunteer.Core.QueriesExtensions;

namespace AnimalVolunteer.Species.Application.Queries.GetFiltredPaginatedAllSpecies;

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
