using AnimalVolunteer.Core.DTOs.Discussions;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Discussions.Application.Interfaces;
public interface IReadOnlyRepository
{
    Result<DiscussionDto, Error> GetDiscussionByRelatedId(Guid id);
}
