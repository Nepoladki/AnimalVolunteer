namespace AnimalVolunteer.Application.FileProvider;

public record FileData(
    Stream FileStream,
    string BucketName,
    string ObjectName);
