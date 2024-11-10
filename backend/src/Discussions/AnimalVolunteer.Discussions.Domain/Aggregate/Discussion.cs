using AnimalVolunteer.Discussions.Domain.Aggregate.Entities;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;
using System.Reflection.Metadata;

namespace AnimalVolunteer.Discussions.Domain.Aggregate;

public sealed class Discussion : CSharpFunctionalExtensions.Entity<DiscussionId>
{
    // EF Core ctor
    private Discussion() { }

    private  List<Message> _messages = default!;
    private List<Guid> _usersIds { get; set; } = default!;
    public IReadOnlyList<Message> Messages => _messages;
    public IReadOnlyList<Guid> UsersIds => _usersIds;
    public Guid RelationId { get; }
    public bool Closed { get; private set; }
}

