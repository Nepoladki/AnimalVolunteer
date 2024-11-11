using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Accounts.Application.Queries.GetUserInfo;

public record GetUserInfoQuery(Guid UserId) : IQuery;

