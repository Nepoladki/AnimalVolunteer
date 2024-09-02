using AnimalVolunteer.Application.Features.Files.Delete;
using AnimalVolunteer.Application.Features.Files.GetUrl;
using AnimalVolunteer.Application.FileProvider;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Interfaces;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(UploadFileData fileData, CancellationToken cancellationToken);
    Task<Result<string, Error>> GetFileUrl(DownloadFileRequest fileData, CancellationToken cancellationToken);
    Task<Result<bool, Error>> DeleteFile(DeleteFileRequest fileData, CancellationToken cancellationToken);
}
