using AnimalVolunteer.Application.DTOs.SpeciesManagement;
using AnimalVolunteer.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.SpeciesManagement.Queries;

public class GetAllSpeciesQueryHandler : 
    IQueryHandler<IReadOnlyList<SpeciesDto>, GetAllSpeciesQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetAllSpeciesQueryHandler> _logger;
    public GetAllSpeciesQueryHandler(
        ILogger<GetAllSpeciesQueryHandler> logger, 
        IReadDbContext readDbContext)
    {
        _logger = logger;
        _readDbContext = readDbContext;
    }
    public async Task<IReadOnlyList<SpeciesDto>> Handle(
        GetAllSpeciesQuery query, 
        CancellationToken cancellationToken)
    {
        var species = _readDbContext.;
    }
}
