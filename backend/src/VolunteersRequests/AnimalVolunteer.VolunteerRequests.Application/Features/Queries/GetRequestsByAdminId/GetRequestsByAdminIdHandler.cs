using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Models;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestsByAdminId;

public class GetRequestsByAdminIdHandler : 
    IQueryHandler<Result<PagedList<VolunteerRequestDto>, ErrorList>, GetRequestsByAdminIdQuery>
{
    private readonly IReadOnlyRepository _repository;
    private readonly IValidator<GetRequestsByAdminIdQuery> _validator;

    public GetRequestsByAdminIdHandler(
        IReadOnlyRepository repository,
        IValidator<GetRequestsByAdminIdQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }


    public async Task<Result<PagedList<VolunteerRequestDto>, ErrorList>> Handle(
        GetRequestsByAdminIdQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        return await _repository.GetRequestsByAdminId(
            query.AdminId, query.Page, query.PageSize, cancellationToken, query.Status);
    }
}

