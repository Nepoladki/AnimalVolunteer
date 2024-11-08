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
        UserId userId, 
        AdminId adminId, 
        DiscussionId discussionId) : base(id)
    {
        UserId = userId;
        AdminId = adminId;
        DiscussionId = discussionId;
        Status = VolunteerRequestStatus.Created;
        RejectionComment = null;
        CreatedAt = DateTime.UtcNow;
    }
    public UserId UserId { get; } = null!;
    public AdminId AdminId { get; private set; } = null!;
    public DiscussionId DiscussionId { get; private set; } = null!;
    public VolunteerRequestStatus Status { get; private set; }
    public string? RejectionComment { get; private set; }
    public DateTime CreatedAt { get; }

    public static VolunteerRequest Create(
        VolunteerRequestId id,
        UserId userId, 
        AdminId adminId, 
        DiscussionId discussionId) =>
            new(id, userId, adminId, discussionId);

    /// <summary>
    /// Sets status of request to Submitted.
    /// </summary>
    public void Submit() => Status = VolunteerRequestStatus.Submitted;


    /// <summary>
    /// Sets status to RevisionRequired, which means that candidate
    /// must get aquainted with comment RejectionComment and amend request.
    /// </summary>
    public void SendOnRevision() => Status = VolunteerRequestStatus.RevisionRequired;

    /// <summary>
    /// Sets status to Rejected, 
    /// which means no more actions could be made with this request, except deleting
    /// </summary>
    /// <param name="rejectionComment"></param>
    /// <returns>CSharpFunctionalExtensions.UnitResult`Error</returns>
    public UnitResult<Error> Reject(string rejectionComment)
    {
        if (string.IsNullOrWhiteSpace(rejectionComment))
            return Errors.VolunteerRequests.RejectionMessageEmpty(Id);
        
        RejectionComment = rejectionComment;
        Status = VolunteerRequestStatus.Rejected;

        return UnitResult.Success<Error>();
    }
    /// <summary>
    /// Sets status to Approved. Afrer, a request must be made to create VolunteerAccount for user.
    /// </summary>
    public void ApproveRequest() => Status = VolunteerRequestStatus.Approved;
}

