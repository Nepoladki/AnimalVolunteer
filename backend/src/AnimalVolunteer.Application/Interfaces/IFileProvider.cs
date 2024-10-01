using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
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
        FileInfoDto fileInfo, 
        CancellationToken cancellationToken);
    Task<UnitResult<Error>> DeleteFile(
        FileInfoDto fileInfo, 
        CancellationToken cancellationToken);
}
