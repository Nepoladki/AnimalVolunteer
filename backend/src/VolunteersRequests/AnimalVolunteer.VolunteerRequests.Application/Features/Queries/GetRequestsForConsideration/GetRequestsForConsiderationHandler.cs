using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestsForConsideration;

public class GetRequestsForConsiderationHandler 
    : IQueryHandler<Result<IReadOnlyList<VolunteerRequestDto>, ErrorList>, GetRequestsForConsiderationQuery>
{
    private readonly IValidator<GetRequestsForConsiderationQuery> _validator;
    private readonly ILinq2dbConnection _linq2DbConnection;

    public GetRequestsForConsiderationHandler(
        IValidator<GetRequestsForConsiderationQuery> validator,
        ILinq2dbConnection linq2DbConnection)
    {
        _validator = validator;
        _linq2DbConnection = linq2DbConnection;
    }

    public async Task<Result<IReadOnlyList<VolunteerRequestDto>, ErrorList>> Handle(
        GetRequestsForConsiderationQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
    }
}

