using AnimalVolunteer.Application.DTOs.VolunteerManagement.Pet;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Application.Models;
using FluentValidation;
using System.Reflection.Metadata.Ecma335;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetsFilteredPaginated;

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
        var petsQuery = _readDbContext.Pets;

        petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            p => p.Name.Contains(query.Name!));

        petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Color),
            p => p.Color.Contains(query.Color!));

        petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Country),
            p => p.Country.Contains(query.Country!));

        petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.City),
            p => p.City.Contains(query.City!));

        petsQuery.WhereIf(
            query.VolunteerId.GetValueOrDefault(Guid.Empty) != Guid.Empty,
            p => p.VolunteerId == query.VolunteerId);

        petsQuery.WhereIf(
            query.SpeciesId.GetValueOrDefault(Guid.Empty) != Guid.Empty,
            p => p.SpeciesId == query.SpeciesId);

        petsQuery.WhereIf(
            query.BreedId.GetValueOrDefault(Guid.Empty) != Guid.Empty,
            p => p.BreedId == query.BreedId);

        petsQuery.WhereIf(
            query.AgeFrom.HasValue,
            p => ((DateTime.Today - p.BirthDate.ToDateTime(TimeOnly.MinValue)).TotalDays / 365)
            >= query.AgeFrom!.Value);

        petsQuery.WhereIf(
            query.AgeTo.HasValue,
            p => ((DateTime.Today - p.BirthDate.ToDateTime(TimeOnly.MinValue)).TotalDays / 365)
            <= query.AgeTo!.Value);

        petsQuery.WhereIf(
            query.WeightFrom.HasValue, p => p.Weight >= query.WeightFrom!);

        petsQuery.WhereIf(
            query.WeightTo.HasValue, p => p.Weight <= query.WeightTo!);

        petsQuery.WhereIf(
            query.HeightFrom.HasValue, p => p.Height >= query.HeightFrom!);

        petsQuery.WhereIf(
            query.HeightTo.HasValue, p => p.Height <= query.HeightTo!);

        return await petsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
