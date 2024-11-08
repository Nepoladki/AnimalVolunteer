using AnimalVolunteer.Discussions.Domain.Aggregate.Entities;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Discussions.Domain.Aggregate;

public class Discussion : CSharpFunctionalExtensions.Entity<DiscussionId>
{
    private readonly List<Message> _messages = default!;


}

