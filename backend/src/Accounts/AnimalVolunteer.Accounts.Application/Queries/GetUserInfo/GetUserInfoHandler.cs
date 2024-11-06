using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Queries.GetUserInfo;

public class GetUserInfoHandler : IQueryHandler<Result<UserAccountDto, ErrorList>, GetUserInfoQuery>
{
    private readonly IAccountManager _accountManager;
    private readonly IValidator<GetUserInfoQuery> _validator;

    public GetUserInfoHandler(
        IAccountManager accountManager, 
        IValidator<GetUserInfoQuery> validator)
    {
        _accountManager = accountManager;
        _validator = validator;
    }

    public async Task<Result<UserAccountDto, ErrorList>> Handle(
        GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(query);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var userAccount = _accountManager.
    }
}

