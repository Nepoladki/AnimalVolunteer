using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
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
        UserId userId,
        VolunteerInfo volunteerInfo) : base(id)
    {
        UserId = userId;
        AdminId = Guid.Empty;
        VolunteerInfo = volunteerInfo;
        DiscussionId = Guid.Empty;
        Status = VolunteerRequestStatus.Created;
        RejectionComment = null;
        LastRejectionAt = null;
        CreatedAt = DateTime.UtcNow;
    }
    public UserId UserId { get; } = null!;
    public AdminId AdminId { get; private set; }
    public DiscussionId DiscussionId { get; private set; }
    public VolunteerInfo  VolunteerInfo { get; private set; }
    public VolunteerRequestStatus Status { get; private set; }
    public string? RejectionComment { get; private set; }
    public DateTime? LastRejectionAt { get; private set; }
    public DateTime CreatedAt { get; }

    public static VolunteerRequest Create(
        VolunteerRequestId id,
        UserId userId,
        VolunteerInfo volunteerInfo) =>
            new(id, userId, volunteerInfo);

    /// <summary>
    /// Sets admin and discussion id's and changes status of request to Submitted.
    /// (Meaning that now request is on consideration)
    /// </summary>
    public UnitResult<Error> TakeOnCosideration(AdminId adminId, DiscussionId discussionId)
    {
        if (Status != (VolunteerRequestStatus.Created))
            return Errors.VolunteerRequests.WrongStatusChange();

        AdminId = adminId;
        DiscussionId = discussionId;

        Status = VolunteerRequestStatus.OnConsideration;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Sets status to RevisionRequired, which means that candidate
    /// must get aquainted with RejectionComment and update request.
    /// </summary>
    /// <param name="rejectionComment"></param>
    /// <returns>CSharpFunctionalExtensions.UnitResult`Error</returns>
    public UnitResult<Error> SendOnRevision(string rejectionComment)
    {
        if (Status != VolunteerRequestStatus.OnConsideration)
            return Errors.VolunteerRequests.WrongStatusChange();

        RejectionComment = rejectionComment;
        Status = VolunteerRequestStatus.RevisionRequired;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Update information about volunteer. 
    /// Request must be taken on consideration by admin after being updated.
    /// </summary>
    /// <param name="volunteerInfo"></param>
    /// <returns></returns>
    public UnitResult<Error> UpdateRequest(VolunteerInfo volunteerInfo)
    {
        if (Status != VolunteerRequestStatus.RevisionRequired)
            return Errors.VolunteerRequests.WrongStatusChange();

        VolunteerInfo = volunteerInfo;
        Status = VolunteerRequestStatus.Updated;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Take updated request on consideration by the same admin.
    /// </summary>
    /// <returns></returns>
    public UnitResult<Error> TakeOnConsiderationAfterRevision()
    {
        if (Status != VolunteerRequestStatus.Updated)
            return Errors.VolunteerRequests.WrongStatusChange();

        Status = VolunteerRequestStatus.OnConsideration;

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
        if (Status != VolunteerRequestStatus.OnConsideration)
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
        if (Status != VolunteerRequestStatus.OnConsideration)
            return Errors.VolunteerRequests.WrongStatusChange();
    
        Status = VolunteerRequestStatus.Approved;

        return UnitResult.Success<Error>();
    }
}

