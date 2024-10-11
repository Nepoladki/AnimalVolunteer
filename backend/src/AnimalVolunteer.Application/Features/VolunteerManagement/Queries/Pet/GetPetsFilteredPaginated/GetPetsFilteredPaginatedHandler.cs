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
            query.AgeFrom.HasValue || query.AgeTo.HasValue,
            p => (DateTime.Today - p.BirthDate.ToDateTime(TimeOnly.MinValue) >= TimeSpan.FromDays(query.AgeFrom!.Value / 365))&&
            (DateTime.Today - p.BirthDate.ToDateTime(TimeOnly.MinValue) <= TimeSpan.FromDays(query.AgeTo!.Value / 365)));

        petsQuery.WhereIf(
            query.WeightFrom.HasValue || query.WeightTo.HasValue,
            p =>
                (p.Weight >= query.WeightFrom.GetValueOrDefault(0)) &&
                (p.Weight <= query.WeightTo.GetValueOrDefault(int.MaxValue)));

        petsQuery.WhereIf(
            query.HeightFrom.HasValue || query.HeightTo.HasValue,
            p =>
                (p.Height >= query.HeightFrom.GetValueOrDefault(0)) &&
                (p.Height <= query.HeightTo.GetValueOrDefault(int.MaxValue)));

        return await petsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

    }

    private static bool AgeFilterPredicate(DateOnly birthDate, int? ageFrom, int? ageTo)
    {
        var today = DateTime.Today;
        var ageInYears = (today - birthDate.ToDateTime(TimeOnly.MinValue)).TotalDays / 365;

        if (ageFrom.HasValue && ageTo.HasValue)
        {
            return ageInYears >= ageFrom.Value && ageInYears <= ageTo.Value;
        }

        if (ageFrom.HasValue)
        {
            return ageInYears >= ageFrom.Value;
        }

        if (ageTo.HasValue)
        {
            return ageInYears <= ageTo.Value;
        }

        return true; // No filtering on age
    }
}
