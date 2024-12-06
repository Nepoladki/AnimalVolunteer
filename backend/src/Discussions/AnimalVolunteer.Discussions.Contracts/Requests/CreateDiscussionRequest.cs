namespace AnimalVolunteer.Discussions.Contracts.Requests;

public record CreateDiscussionRequest(Guid RelatedId, Guid UserId, Guid AdminId);

