using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Application.Queries.GetUserInfo;

public class GetUserInfoHandler :
    IQueryHandler<Result<UserDto, ErrorList>, GetUserInfoQuery>
{
    private readonly IValidator<GetUserInfoQuery> _validator;
    private readonly IReadDbContext _readDbContext;

    public GetUserInfoHandler(
        IValidator<GetUserInfoQuery> validator, IReadDbContext readDbContext)
    {
        _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<Result<UserDto, ErrorList>> Handle(
        GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(query);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var user = await _readDbContext.Users
            .Include(u => u.AdminAccount)
            .Include(u => u.VolunteerAccount)
            .Include(u => u.ParticipantAccount)
            .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken);

        if (user is null)
            return Errors.Accounts.UserNotFoud(query.UserId).ToErrorList();

        return user;
    }
}

