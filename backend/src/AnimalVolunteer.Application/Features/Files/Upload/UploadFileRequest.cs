using AnimalVolunteer.Application.FileProvider;

namespace AnimalVolunteer.Application.Features.Files.Upload;

public record UploadFileRequest(
    Stream FileStream,
    string BucketName,
    string ObjectName);
