using Amazon.S3;
using Amazon.S3.Model;
using FileServiceAPI.Endpoints;

namespace FileServiceAPI.Features;

public static class GetPartUploadPresignedUrl
{
    private record UploadPartPresignedUrlRequest(
        string UploadId,
        int PartNumber);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/presigned-part", Handler);
        }
    }

    private static async Task<IResult> Handler(
        UploadPartPresignedUrlRequest request,
        IAmazonS3 s3Client,
        CancellationToken cancellationToken)
    {
        try
        {
            var key = Guid.NewGuid();

            var urlRequest = new GetPreSignedUrlRequest
            {
                BucketName = "bucket",
                Key = $"videos/{key}",
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(1),
                Protocol = Protocol.HTTP,
                UploadId = request.UploadId,
                PartNumber = request.PartNumber
            };

            var url = await s3Client.GetPreSignedURLAsync(urlRequest);

            return Results.Ok(new { key, url }); 
        }
        catch (AmazonS3Exception ex)
        {
            return Results.BadRequest($"Error occured while generating part-upload URL: {ex.Message}");
        }
    }
}
