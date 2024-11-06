using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.Factories;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Queries.GetUserInfo;

public class GetUserInfoHandler : IQueryHandler<Result<UserAccountDto, ErrorList>, GetUserInfoQuery>
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;
    private readonly IValidator<GetUserInfoQuery> _validator;

    public GetUserInfoHandler(
        SqlConnectionFactory sqlConnectionFactory,
        IValidator<GetUserInfoQuery> validator)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _validator = validator;
    }

    public async Task<Result<UserAccountDto, ErrorList>> Handle(
        GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(query);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        using var connection = _sqlConnectionFactory.Create();

        var query = """"""
    }
}

