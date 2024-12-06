﻿using AnimalVolunteer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AnimalVolunteer.Core.Extensions;

public static class EfCoreQueriesExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {

        var totalCount = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>
        {
            Items = items,
            PageSize = pageSize,
            Page = page,
            TotalCount = totalCount
        };
    }
}