using FileServiceAPI.Core;
using MongoDB.Driver;

namespace FileServiceAPI.MongoDataAccess;

public class FilesMongoDbContext(IMongoClient mongoClient)
{
    private const string FILES_DATABASE = "files_db";
    private const string FILES_COLLECTION = "files";

    private readonly IMongoDatabase _database = mongoClient.GetDatabase(FILES_DATABASE);

    public IMongoCollection<FileData> Files => _database.GetCollection<FileData>(FILES_COLLECTION);
}
