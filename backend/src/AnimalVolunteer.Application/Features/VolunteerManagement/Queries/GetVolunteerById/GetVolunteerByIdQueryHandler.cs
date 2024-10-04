using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdQueryHandler : 
    IQueryHandler<Result<VolunteerDto, ErrorList>, GetVolunteerByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetVolunteerByIdQueryHandler> _logger;

    public GetVolunteerByIdQueryHandler(
        IReadDbContext readDbContext, 
        ILogger<GetVolunteerByIdQueryHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<VolunteerDto, ErrorList>> Handle(
        GetVolunteerByIdQuery query, CancellationToken cancellationToken)
    {
        var volunteerDto = await _readDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == query.Id, cancellationToken);

        if (volunteerDto is null)
        {
            _logger.LogWarning("Failed to query volunteer with id {id}", query.Id);
            return Errors.General.NotFound(query.Id).ToErrorList();
        }

        _logger.LogInformation("Volunteer with id {id} queried", query.Id);

        return volunteerDto;
    }
}
