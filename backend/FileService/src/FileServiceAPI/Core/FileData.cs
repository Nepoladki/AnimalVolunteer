using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;

namespace FileServiceAPI.Core;

public class FileData
{
    [BsonId]
    [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
    public Guid Id { get; init; }
    public required string StoragePath { get; init; }
    public required DateTime DateTime { get; init; }
    public required long Size { get; init; }
    public required string ContentType { get; init; }
}
