namespace AnimalVolunteer.Application.FileProvider;

public record UploadFileData(
    Stream FileStream,
    string BucketName,
    string ObjectName);
