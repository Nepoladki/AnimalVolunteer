using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Features.Files.Delete;
using AnimalVolunteer.Application.Features.Files.GetUrl;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Interfaces;

public interface IFileProvider
{
    Task<UnitResult<Error>> UploadFile(
        Stream file, 
        string bucketName, 
        string objectName,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<UploadingFileDto> petPhotos,
        string bucketName,
        CancellationToken cancellationToken);
    Task<Result<string, Error>> GetFileUrl(
        DownloadFileRequest fileData, 
        CancellationToken cancellationToken);
    Task<UnitResult<Error>> DeleteFile(
        DeleteFileRequest fileData, 
        CancellationToken cancellationToken);
}
