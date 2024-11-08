using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Queries.GetUserInfo;

public class GetUserInfoValidator : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoValidator()
    {
        RuleFor(q => q.UserId).NotEmpty();
    }
}

