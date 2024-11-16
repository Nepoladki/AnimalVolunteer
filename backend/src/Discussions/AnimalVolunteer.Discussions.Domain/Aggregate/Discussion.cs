using AnimalVolunteer.Discussions.Domain.Aggregate.Entities;
using AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.Discussions.Domain.Aggregate;

public sealed class Discussion : CSharpFunctionalExtensions.Entity<DiscussionId>
{
    // EF Core ctor
    private Discussion() { }

    private Discussion(Guid relationId, IEnumerable<Guid> users)
    {
        RelationId = relationId;
        _usersIds = users.ToArray();
    }

    private readonly List<Message> _messages = default!;

    private readonly Guid[] _usersIds = new Guid[2];
    public IReadOnlyList<Message> Messages => _messages;
    public IList<Guid> UsersIds => _usersIds;
    public Guid RelationId { get; }
    public bool IsOpened { get; private set; } = true;

    public void Close() => IsOpened = false;

    public static Result<Discussion, Error> Create(Guid relationId, IEnumerable<Guid> users)
    {
        if (users.Count() != 2)
            return Errors.Disscussions.IncorrectUsersQuantity();

        if (users.Any(u => u == Guid.Empty))
            return Errors.Disscussions.InvalidDiscussionUsers();

        return new Discussion(relationId, users.ToList());
    }

    public UnitResult<Error> Message(Message message, Guid userId) 
    {
        var openedResult = IsDiscusionOpened();
        if (openedResult.IsFailure)
            return openedResult.Error;

        var accessResult = IsMessagingAllowed(userId);
        if (accessResult.IsFailure)
            return accessResult.Error;

        _messages.Add(message);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> DeleteMessage(Message message, Guid userId)
    {
        var openedResult = IsDiscusionOpened();
        if (openedResult.IsFailure)
            return openedResult.Error;

        var acccessResult = DoesUserOwnMessage(message.Id, userId);
        if (acccessResult.IsFailure)
            return acccessResult.Error;

        _messages.Remove(message);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> AmendMessage(MessageId messageId, Guid userId, Text newText)
    {
        var openedResult = IsDiscusionOpened();
        if (openedResult.IsFailure)
            return openedResult.Error;

        var message = _messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
            return Errors.Disscussions.MessageNotFound(messageId);

        var acccessResult = DoesUserOwnMessage(message.Id, userId);
        if (acccessResult.IsFailure)
            return acccessResult.Error;

        message.AmendText(newText);

        return UnitResult.Success<Error>();
    }

    private UnitResult<Error> IsMessagingAllowed(Guid userId)
    {
        if (UsersIds.Contains(userId) == false)
            return Errors.Disscussions.MessagingNotAllowed(userId, Id);

        return UnitResult.Success<Error>();
    }

    private UnitResult<Error> DoesUserOwnMessage(MessageId messageId, Guid userId)
    {
        if (messageId != userId)
            return Errors.Disscussions.AccesDenied();

        return UnitResult.Success<Error>();
    }

    private UnitResult<Error> IsDiscusionOpened()
    {
        if (IsOpened == false)
            return Errors.Disscussions.DiscussionClosed(Id);

        return UnitResult.Success<Error>();
    }

}

