using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Interfaces;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}
