using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Domain;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestByUserId;

public class GetRequestByUserIdHandler : 
    IQueryHandler<Result<VolunteerRequestDto, ErrorList>, GetRequestByUserIdQuery>
{
    private readonly IReadOnlyRepository _repository;
    private readonly IValidator<GetRequestByUserIdQuery> _validator;

    public GetRequestByUserIdHandler(
        IReadOnlyRepository repository, 
        IValidator<GetRequestByUserIdQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<VolunteerRequestDto, ErrorList>> Handle(
        GetRequestByUserIdQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        return await _repository.GetRequestByUserId(query.UserId, cancellationToken);
    }
}

