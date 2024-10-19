namespace AnimalVolunteer.Core.DTOs;

public record FileData(
    Stream FileStream,
    string BucketName,
    string ObjectName);
