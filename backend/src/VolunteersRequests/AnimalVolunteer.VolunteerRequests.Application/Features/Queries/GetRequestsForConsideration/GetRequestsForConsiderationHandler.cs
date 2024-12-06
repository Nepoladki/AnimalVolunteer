using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Models;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestsForConsideration;

public class GetRequestsForConsiderationHandler 
    : IQueryHandler<Result<PagedList<VolunteerRequestDto>, ErrorList>, GetRequestsForConsiderationQuery>
{
    private readonly IValidator<GetRequestsForConsiderationQuery> _validator;
    private readonly IReadOnlyRepository _repository;

    public GetRequestsForConsiderationHandler(
        IValidator<GetRequestsForConsiderationQuery> validator,
        IReadOnlyRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result<PagedList<VolunteerRequestDto>, ErrorList>> Handle(
        GetRequestsForConsiderationQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        return await _repository.GetRequestsForConsideration(query.Page, query.PageSize, cancellationToken);
    }
}

