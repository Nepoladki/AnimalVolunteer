using CSharpFunctionalExtensions;
using FileServiceAPI.Core;
using FileServiceAPI.Core.Models;
using FileServiceAPI.Interfaces;
using MongoDB.Driver;

namespace FileServiceAPI.MongoDataAccess;

public class FilesRepository : IFilesRepository
{
    private readonly FilesMongoDbContext _dbContext;

    public FilesRepository(FilesMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, Error>> Add(FileData fileData, CancellationToken cancellationToken)
    {
        await _dbContext.Files.InsertOneAsync(fileData, cancellationToken: cancellationToken);

        return fileData.Id;
    }

    public async Task<IReadOnlyCollection<FileData>> GetDatasByIdList(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        => await _dbContext.Files.Find(f => ids.Contains(f.Id)).ToListAsync(cancellationToken);

    public async Task<UnitResult<Error>> DeleteMany(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        var deleteResult = await _dbContext.Files.DeleteManyAsync(f => ids.Contains(f.Id), cancellationToken);
        if (deleteResult.DeletedCount == 0)
            return Errors.Files.UnableToDelete();

        return UnitResult.Success<Error>();
    }
}
