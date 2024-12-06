using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using AnimalVolunteer.Core.QueriesExtensions;
using AnimalVolunteer.Core.Extensions.Linq2Db;
using AnimalVolunteer.Core.Models;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Domain.Enums;
using AnimalVolunteer.VolunteerRequests.Infrastructure.Linq2db;
using CSharpFunctionalExtensions;
using LinqToDB;
using System.Threading;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Repositories;

public class ReadOnlyRepository : IReadOnlyRepository
{
    private readonly Linq2DbConnection _connection;

    public ReadOnlyRepository(Linq2DbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Result<DateTime?, Error>> GetLastRequestDate(Guid userId, CancellationToken token = default)
    {
        var request = await _connection.VolunteerRequests.FirstOrDefaultAsync(r => r.UserId == userId, token);
        if (request is null)
            return Errors.General.NotFound(userId);

        return request.LastRejectionAt;
    }

    public Task<bool> VolunteerRequestExistsByUserId(Guid userId, CancellationToken token = default) =>
         _connection.VolunteerRequests.AnyAsync(r => r.UserId == userId, token);

    public async Task<PagedList<VolunteerRequestDto>> GetRequestsForConsideration(
        int Page, int PageSize, CancellationToken cancellationToken)
    {
        var requests = from vr in _connection.VolunteerRequests
                       where vr.AdminId == Guid.Empty
                       select vr;

        return await requests.ToPagedList(Page, PageSize, cancellationToken);
    }

    public Task<PagedList<VolunteerRequestDto>> GetRequestsByAdminId(
        Guid AdminId,
        int Page,
        int PageSize,
        CancellationToken cancellationToken,
        VolunteerRequestStatus? Status)
    {
        var requests = from vr in _connection.VolunteerRequests
                       where vr.AdminId == AdminId
                       select vr;

        return requests
            .WhereIf(Status is not null, vr => vr.Status == Status.ToString())
            .ToPagedList(Page, PageSize, cancellationToken);
    }

    public async Task<Result<VolunteerRequestDto, ErrorList>> GetRequestByUserId(
        Guid UserId, CancellationToken cancellationToken)
    {
        var request = await _connection.VolunteerRequests
            .FirstOrDefaultAsync(vr => vr.UserId == UserId, cancellationToken);
        if (request is null)
            return Errors.General.NotFound(UserId).ToErrorList();

        return request;
    }
}

