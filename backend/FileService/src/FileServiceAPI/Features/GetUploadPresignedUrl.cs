using Amazon.S3;
using Amazon.S3.Model;
using FileServiceAPI.Endpoints;

namespace FileServiceAPI.Features;

public static class GetUploadPresignedUrl
{
    private record UploadPresignedUrlRequest(
        string FileName,
        string ContentType,
        long Size);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/presigned", Handler);
        }
    }

    private static async Task<IResult> Handler(
        UploadPresignedUrlRequest request,
        IAmazonS3 s3Client,
        CancellationToken cancellationToken)
    {
        try
        {
            var key = $"{request.ContentType}/{Guid.NewGuid()}";

            var urlRequest = new GetPreSignedUrlRequest
            {
                BucketName = "bucket",
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(1),
                ContentType = request.ContentType,
                Protocol = Protocol.HTTP,
                Metadata =
                {
                    ["file-name"] = request.FileName
                }
            };

            var url = await s3Client.GetPreSignedURLAsync(urlRequest);

            return Results.Ok(url);
        }
        catch (AmazonS3Exception ex)
        {
            return Results.BadRequest($"Error occured while generating upload URL: {ex.Message}");
        }
    }
}
