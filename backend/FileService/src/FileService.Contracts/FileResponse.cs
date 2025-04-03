namespace FileService.Contracts;

public record FileResponse(Guid Id, string PresignedUrl);
