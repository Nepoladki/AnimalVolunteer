using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Domain.Enums;
using CSharpFunctionalExtensions;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.VolunteerRequests.Domain;

public sealed class VolunteerRequest
{
    // EF Core ctor 
    private VolunteerRequest() { }
    private VolunteerRequest(UserId userId, AdminId adminId, DiscussionId discussionId)
    {
        RequestId = VolunteerRequestId.Create();
        UserId = userId;
        AdminId = adminId;
        DiscussionId = discussionId;
        Status = VolunteerRequestStatus.Created;
        RejectionComment = null;
        CreatedAt = DateTime.UtcNow;
    }
    public VolunteerRequestId RequestId { get; }
    public UserId UserId { get; } = null!;
    public AdminId AdminId { get; private set; } = null!;
    public DiscussionId DiscussionId { get; private set; } = null!;
    public VolunteerRequestStatus Status { get; private set; }
    public string? RejectionComment { get; private set; }
    public DateTime CreatedAt { get; }

    public static VolunteerRequest Create(
        UserId userId, 
        AdminId adminId, 
        DiscussionId discussionId) =>
            new(userId, adminId, discussionId);

    /// <summary>
    /// Sets status of request to Submitted, 
    /// then provides way of transfering it to admin's consideration
    /// </summary>
    public void Submit()
    {
        // Request transfer logic

        Status = VolunteerRequestStatus.Submitted;
    }

    /// <summary>
    /// Sets status to RevisionRequired, which means that candidate
    /// must get aquainted with comment RejectionComment and amend request
    /// </summary>
    /// <param name="comment"></param>
    /// <returns>CSharpFunctionalExtensions.UnitResult</returns>
    public UnitResult<Error> SendOnRevision(string comment)
    {
        // Request transfer logic

        Status = VolunteerRequestStatus.RevisionRequired;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Sets status to Rejected, 
    /// which means no more actions could be made with this request, except deleting
    /// </summary>
    /// <param name="rejectionComment"></param>
    /// <returns>CSharpFunctionalExtensions.UnitResult</returns>
    public UnitResult<Error> Reject(string rejectionComment)
    {
        if (string.IsNullOrWhiteSpace(rejectionComment))
            return Errors.VolunteerRequests.RejectionMessageEmpty(RequestId);
        
        RejectionComment = rejectionComment;
        Status = VolunteerRequestStatus.Rejected;

        return UnitResult.Success<Error>();
    }
    /// <summary>
    /// Sets status to Approved,
    /// then requests to create VolunteerAccount
    /// </summary>
    /// <returns></returns>
    public UnitResult<Error> ApproveRequest()
    {
        Status = VolunteerRequestStatus.Approved;

        // Request to create VolunteerAccout (by contract?)

        return UnitResult.Success<Error>();
    }
}

