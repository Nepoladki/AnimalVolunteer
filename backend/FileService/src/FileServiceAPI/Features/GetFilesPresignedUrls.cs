using Amazon.S3.Model;
using Amazon.S3;
using FileServiceAPI.Endpoints;
using FileServiceAPI.Interfaces;
using FileService.Contracts;

namespace FileServiceAPI.Features;

public class GetFilesPresignedUrls
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/presigned-urls", Handler);
        }
    }

    private static async Task<IResult> Handler(
        IEnumerable<Guid> fileIds,
        IAmazonS3 s3Client,
        IFilesRepository filesRepository,
        CancellationToken cancellationToken)
    {
        var files = await filesRepository.GetDatasByIdList(fileIds, cancellationToken);

        List<FileResponse> response = new List<FileResponse>();

        foreach (var file in files)
        {
            var urlRequest = new GetPreSignedUrlRequest
            {
                BucketName = "bucket",
                Key = file.StoragePath,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(10),
                Protocol = Protocol.HTTP
            };

            var url = await s3Client.GetPreSignedURLAsync(urlRequest);

            response.Add(new FileResponse(file.Id, url));
        }

        return Results.Ok(response);
    }
}
