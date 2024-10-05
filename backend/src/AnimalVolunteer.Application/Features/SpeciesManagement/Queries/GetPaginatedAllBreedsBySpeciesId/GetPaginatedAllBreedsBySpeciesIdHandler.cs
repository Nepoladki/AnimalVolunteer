using AnimalVolunteer.Application.DTOs.SpeciesManagement;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Features.SpeciesManagement.Queries.GetPaginatedAllBreedsBySpeciesId;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Application.Models;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace AnimalVolunteer.API.Controllers.Species;

public class GetPaginatedAllBreedsBySpeciesIdHandler : 
    IQueryHandler<Result<PagedList<BreedDto>, Error>, GetPaginatedAllBreedsBySpeciesIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetPaginatedAllBreedsBySpeciesIdHandler> _logger;

    public GetPaginatedAllBreedsBySpeciesIdHandler(
        ILogger<GetPaginatedAllBreedsBySpeciesIdHandler> logger, 
        IReadDbContext readDbContext)
    {
        _logger = logger;
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<BreedDto>, Error>> Handle(
        GetPaginatedAllBreedsBySpeciesIdQuery query, 
        CancellationToken cancellationToken)
    {
        var species = await _readDbContext.Species
            .AnyAsync(s => s.Id == query.SpeciesId, cancellationToken);
        if (species == false)
        {
            _logger.LogInformation("Requested species with id {id} was not found", query.SpeciesId);
            return Errors.General.NotFound(query.SpeciesId);
        }

        return await _readDbContext.Breeds.Where(b => b.SpeciesId == query.SpeciesId)
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}