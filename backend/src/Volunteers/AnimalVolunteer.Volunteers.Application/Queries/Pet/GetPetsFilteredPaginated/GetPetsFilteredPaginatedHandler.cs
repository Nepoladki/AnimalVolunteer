using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Volunteers.Pet;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Models;
using AnimalVolunteer.Core.QueriesExtensions;
using AnimalVolunteer.Volunteers.Application.Interfaces;
using System.Linq.Expressions;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.GetPetsFilteredPaginated;

public class GetPetsFilteredPaginatedHandler :
    IQueryHandler<PagedList<PetDto>, GetPetsFilteredPaginatedQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetsFilteredPaginatedHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<PetDto>> Handle(
        GetPetsFilteredPaginatedQuery query, CancellationToken cancellationToken)
    {
        var petsQuery = ApplyFilters(_readDbContext.Pets, query);

        var keySelector = SortByProperty(query.SortBy);

        petsQuery = query.SortDirection?.ToLower() == "desc"
            ? petsQuery.OrderByDescending(keySelector)
            : petsQuery.OrderBy(keySelector);

        return await petsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
    private static Expression<Func<PetDto, object>> SortByProperty(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return volunteer => volunteer.Id;

        Expression<Func<PetDto, object>> keySelector = sortBy?.ToLower() switch
        {
            "name" => p => p.Name,
            "color" => p => p.Color,
            "weight" => p => p.Weight,
            "height" => p => p.Height,
            "age" => p => p.BirthDate,
            "country" => p => p.Country,
            "city" => p => p.City,
            _ => p => p.Id
        };

        return keySelector;
    }

    private static IQueryable<PetDto> ApplyFilters(
        IQueryable<PetDto> dbQuery, GetPetsFilteredPaginatedQuery query)
    {
        return dbQuery
            .WhereIf(!string.IsNullOrWhiteSpace(query.Name), p => p.Name.Contains(query.Name!))
            .WhereIf(!string.IsNullOrWhiteSpace(query.Color), p => p.Color.Contains(query.Color!))
            .WhereIf(!string.IsNullOrWhiteSpace(query.Country), p => p.Country.Contains(query.Country!))
            .WhereIf(!string.IsNullOrWhiteSpace(query.City), p => p.City.Contains(query.City!))
            .WhereIf(query.VolunteerId.GetValueOrDefault(Guid.Empty) != Guid.Empty, p => p.VolunteerId == query.VolunteerId)
            .WhereIf(query.SpeciesId.GetValueOrDefault(Guid.Empty) != Guid.Empty, p => p.SpeciesId == query.SpeciesId)
            .WhereIf(query.BreedId.GetValueOrDefault(Guid.Empty) != Guid.Empty, p => p.BreedId == query.BreedId)
            .WhereIf(query.AgeFrom.HasValue, p => (DateTime.Today - p.BirthDate.ToDateTime(TimeOnly.MinValue)).TotalDays / 365 >= query.AgeFrom!.Value)
            .WhereIf(query.AgeTo.HasValue, p => (DateTime.Today - p.BirthDate.ToDateTime(TimeOnly.MinValue)).TotalDays / 365 <= query.AgeTo!.Value)
            .WhereIf(query.WeightFrom.HasValue, p => p.Weight >= query.WeightFrom!)
            .WhereIf(query.WeightTo.HasValue, p => p.Weight <= query.WeightTo!)
            .WhereIf(query.HeightFrom.HasValue, p => p.Height >= query.HeightFrom!)
            .WhereIf(query.HeightTo.HasValue, p => p.Height <= query.HeightTo!);
    }
}
