namespace AnimalVolunteer.Application.Features.Files.Delete;

public record DeleteFileRequest(string BucketName, string ObjectName);
