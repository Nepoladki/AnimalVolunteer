using AnimalVolunteer.Core.DTOs.Volunteers.Pet;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Core.Abstractions;

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
        FileInfoDto fileInfo,
        CancellationToken cancellationToken);
    Task<UnitResult<Error>> DeleteFile(
        FileInfoDto fileInfo,
        CancellationToken cancellationToken);
}
