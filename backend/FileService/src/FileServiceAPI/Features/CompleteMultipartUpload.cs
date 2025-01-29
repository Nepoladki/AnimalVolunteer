using Amazon.S3.Model;
using Amazon.S3;
using FileServiceAPI.Endpoints;

namespace FileServiceAPI.Features;

public static class CompleteMultiPartUpload
{
    public record PartETagInfo(int PartNumber, string ETag);
    public record CompleteMultiPartUploadRequest(string UploadId, List<PartETagInfo> Parts);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/{key:guid}complete-multipart", Handler);
        }
    }

    private static async Task<IResult> Handler(
        Guid key,
        CompleteMultiPartUploadRequest request,
        IAmazonS3 s3Client,
        CancellationToken cancellationToken)
    {
        try
        {
            var completeRequest = new CompleteMultipartUploadRequest
            {
                BucketName = "bucket",
                Key = $"videos/{key}",
                UploadId = request.UploadId,
                PartETags = request.Parts.Select(p => new PartETag(p.PartNumber, p.ETag)).ToList()
            };

            var completeResponse = await s3Client.CompleteMultipartUploadAsync(completeRequest, cancellationToken);

            return Results.Ok(new { key, completeResponse.Location });
        }
        catch (AmazonS3Exception ex)
        {
            return Results.BadRequest($"Error occured while completing multipart upload: {ex.Message}");
        }
    }
}
