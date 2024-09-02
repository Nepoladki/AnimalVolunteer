namespace AnimalVolunteer.Application.Features.Files.GetUrl;

public record DownloadFileRequest(string BucketName, string ObjectName);
