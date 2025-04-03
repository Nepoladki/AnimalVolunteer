using Amazon.S3;
using Amazon.S3.Model;
using FileServiceAPI.Endpoints;

namespace FileServiceAPI.Features;

public static class StartMultiPartUpload
{
    private record StartMultiPartUploadRequest(
        string FileName,
        string ContentType,
        long Size);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/multipart", Handler);
        }
    }

    private static async Task<IResult> Handler(
        StartMultiPartUploadRequest request,
        IAmazonS3 s3Client,
        CancellationToken cancellationToken)
    {
        try
        {
            var key = $"{request.ContentType}/{Guid.NewGuid()}";

            var urlRequest = new InitiateMultipartUploadRequest
            {
                BucketName = "bucket",
                Key = key,
                ContentType = request.ContentType,
                Metadata =
                {
                    ["file-name"] = request.FileName
                }
            };

            var url = await s3Client.InitiateMultipartUploadAsync(urlRequest);

            return Results.Ok(url);
        }
        catch (AmazonS3Exception ex)
        {
            return Results.BadRequest($"Error occured while starting multipart upload: {ex.Message}");
        }
    }
}
