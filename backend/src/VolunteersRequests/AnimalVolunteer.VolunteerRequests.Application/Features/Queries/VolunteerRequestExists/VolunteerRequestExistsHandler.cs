using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.VolunteerRequestExists;

public class VolunteerRequestExistsHandler : IQueryHandler<bool, VolunteerRequestExistsQuery>
{
    private readonly IReadOnlyRepository _repository;

    public VolunteerRequestExistsHandler(IReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> Handle(VolunteerRequestExistsQuery query, CancellationToken cancellationToken)
    {
        return _repository.VolunteerRequestExistsByUserId(query.UserId, cancellationToken);
    }
}

    