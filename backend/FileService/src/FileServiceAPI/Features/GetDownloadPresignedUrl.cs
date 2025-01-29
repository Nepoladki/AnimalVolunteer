using Amazon.S3;
using Amazon.S3.Model;
using FileServiceAPI.Endpoints;

namespace FileServiceAPI.Features;

public static class GetDownloadPresignedUrl
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("files/{key:guid}/presigned", Handler);
        }
    }

    private static async Task<IResult> Handler(
        Guid key,
        IAmazonS3 s3Client,
        CancellationToken cancellationToken)
    {
        try
        {
            var urlRequest = new GetPreSignedUrlRequest
            {
                BucketName = "bucket",
                Key = $"videos/{key}",
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(1),
                Protocol = Protocol.HTTP
            };

            var url = await s3Client.GetPreSignedURLAsync(urlRequest);

            return Results.Ok(url);
        }
        catch (AmazonS3Exception ex)
        {
            return Results.BadRequest($"Error occured while fetching download URL: {ex.Message}");
        }
    }
}
