﻿using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.GetPetsFilteredPaginated;

public record GetPetsFilteredPaginatedQuery(
    Guid? VolunteerId,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Name,
    int? AgeFrom,
    int? AgeTo,
    string? Color,
    int? WeightFrom,
    int? WeightTo,
    int? HeightFrom,
    int? HeightTo,
    string? Country,
    string? City,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;
