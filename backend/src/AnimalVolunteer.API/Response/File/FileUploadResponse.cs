namespace AnimalVolunteer.API.Response.File;

public record FileUploadResponse(
    string ObjectName,
    string BucketName);
