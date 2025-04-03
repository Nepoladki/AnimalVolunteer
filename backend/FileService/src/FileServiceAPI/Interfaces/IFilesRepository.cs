using CSharpFunctionalExtensions;
using FileServiceAPI.Core;
using FileServiceAPI.Core.Models;

namespace FileServiceAPI.Interfaces;
public interface IFilesRepository
{
    Task<Result<Guid, Error>> Add(FileData fileData, CancellationToken cancellationToken);
    Task<UnitResult<Error>> DeleteMany(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<FileData>> GetDatasByIdList(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}