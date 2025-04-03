namespace FileService.Contracts;
public record GetFilesPresignedUrlsRequest(IEnumerable<Guid> FilesIds);
