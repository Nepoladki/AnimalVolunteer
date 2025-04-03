using Amazon.S3.Model;
using Amazon.S3;
using FileServiceAPI.Endpoints;
using FileServiceAPI.Interfaces;
using FileServiceAPI.Core;
using Hangfire;
using FileServiceAPI.Infrastructure.Jobs;

namespace FileServiceAPI.Features;

public record PartETagInfo(int PartNumber, string ETag);
public record CompleteMultiPartUploadRequest(string UploadId, List<PartETagInfo> Parts);
public static class CompleteMultiPartUpload
{
    private const string BUCKET_NAME = "bucket";

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/{key}complete-multipart", Handler);
        }
    }

    private static async Task<IResult> Handler(
        string key,
        CompleteMultiPartUploadRequest request,
        IAmazonS3 s3Client,
        IFilesRepository fileRepository,
        CancellationToken cancellationToken)
    {
        try
        {
            var fileId = Guid.NewGuid();

            var jobId = BackgroundJob.Schedule<ConfirmConsistencyJob>(j => 
                j.Execute(fileId, key), TimeSpan.FromSeconds(10));

            var completeRequest = new CompleteMultipartUploadRequest
            {
                BucketName = BUCKET_NAME,
                Key = key,
                UploadId = request.UploadId,
                PartETags = request.Parts.Select(p => new PartETag(p.PartNumber, p.ETag)).ToList()
            };

            var completeResponse = await s3Client.CompleteMultipartUploadAsync(completeRequest, cancellationToken);

            var metaDataRequest = new GetObjectMetadataRequest()
            {
                BucketName = BUCKET_NAME,
                Key = key,
            };

            var metaData = await s3Client.GetObjectMetadataAsync(metaDataRequest, cancellationToken);

            var fileData = new FileData
            {
                Id = fileId,
                StoragePath = key,
                Size = metaData.ContentLength,
                DateTime = DateTime.UtcNow,
                ContentType = metaData.Headers.ContentType
            };

            await fileRepository.Add(fileData, cancellationToken);

            BackgroundJob.Delete(jobId);

            return Results.Ok(new { key, completeResponse.Location });
        }
        catch (AmazonS3Exception ex)
        {
            return Results.BadRequest($"Error occured while completing multipart upload: {ex.Message}");
        }
    }
}
