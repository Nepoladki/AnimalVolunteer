using AnimalVolunteer.Core.DTOs.VolunteerManagement.Pet;

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
