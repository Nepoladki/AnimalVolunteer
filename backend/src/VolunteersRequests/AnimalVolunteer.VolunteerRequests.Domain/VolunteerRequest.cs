using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Domain.Enums;
using CSharpFunctionalExtensions;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.VolunteerRequests.Domain;

public sealed class VolunteerRequest : CSharpFunctionalExtensions.Entity<VolunteerRequestId>
{
    // EF Core ctor 
    private VolunteerRequest(VolunteerRequestId id) : base(id) { }
    private VolunteerRequest(
        VolunteerRequestId id, 
        UserId userId) : base(id)
    {
        UserId = userId;
        AdminId = Guid.Empty;
        DiscussionId = Guid.Empty;
        Status = VolunteerRequestStatus.Created;
        RejectionComment = null;
        LastRejectionAt = null;
        CreatedAt = DateTime.UtcNow;
    }
    public UserId UserId { get; } = null!;
    public AdminId AdminId { get; private set; }
    public DiscussionId DiscussionId { get; private set; }
    public VolunteerRequestStatus Status { get; private set; }
    public string? RejectionComment { get; private set; }
    public DateTime? LastRejectionAt { get; private set; }
    public DateTime CreatedAt { get; }

    public static VolunteerRequest Create(
        VolunteerRequestId id,
        UserId userId) =>
            new(id, userId);

    /// <summary>
    /// Sets admin and discussion id's and changes status of request to Submitted.
    /// (Meaning that now request is on consideration)
    /// </summary>
    public UnitResult<Error> Submit(AdminId adminId, DiscussionId discussionId)
    {
        if (Status != VolunteerRequestStatus.Created)
            return Errors.VolunteerRequests.WrongStatusChange();

        AdminId = adminId;
        DiscussionId = discussionId;

        Status = VolunteerRequestStatus.Submitted;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Sets status to RevisionRequired, which means that candidate
    /// must get aquainted with comment RejectionComment and amend request.
    /// </summary>
    public UnitResult<Error> SendOnRevision()
    {
        if (Status != VolunteerRequestStatus.Submitted)
            return Errors.VolunteerRequests.WrongStatusChange();

        Status = VolunteerRequestStatus.RevisionRequired;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Sets status to Rejected, 
    /// which means no more actions could be made with this request, except deleting
    /// </summary>
    /// <param name="rejectionComment"></param>
    /// <returns>CSharpFunctionalExtensions.UnitResult`Error</returns>
    public UnitResult<Error> Reject(string rejectionComment)
    {
        if (Status != VolunteerRequestStatus.Submitted)
            return Errors.VolunteerRequests.WrongStatusChange();

        if (string.IsNullOrWhiteSpace(rejectionComment))
            return Errors.VolunteerRequests.RejectionMessageEmpty(Id);
        
        RejectionComment = rejectionComment;
        Status = VolunteerRequestStatus.Rejected;
        LastRejectionAt = DateTime.UtcNow;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Sets status to Approved. Afrer, a request must be made to create VolunteerAccount for user.
    /// </summary>
    public UnitResult<Error> ApproveRequest()
    {
        if (Status != VolunteerRequestStatus.Submitted)
            return Errors.VolunteerRequests.WrongStatusChange();
    
        Status = VolunteerRequestStatus.Approved;

        return UnitResult.Success<Error>();
    }
}

