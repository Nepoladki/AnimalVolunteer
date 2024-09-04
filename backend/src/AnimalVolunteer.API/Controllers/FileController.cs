using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.API.Response.File;
using AnimalVolunteer.Application.Features.Files.Delete;
using AnimalVolunteer.Application.Features.Files.Download;
using AnimalVolunteer.Application.Features.Files.GetUrl;
using AnimalVolunteer.Application.Features.Files.Upload;
using AnimalVolunteer.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.API.Controllers;

[Route("api/file")]
public class FileController : ApplicationController
{
    private readonly MinioOptions _minioOptions;
    public FileController(IOptionsSnapshot<MinioOptions> minioOptions)
    {
        _minioOptions = minioOptions.Value;
    }
    [HttpPost]
    public async Task<IActionResult> Upload(
        IFormFile file,
        [FromServices] UploadFileHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var bucketName = _minioOptions.BucketName;

        var objectName = Guid.NewGuid().ToString();

        var uploadResult = await handler.Upload(
            new UploadFileRequest(stream, bucketName, objectName),
            cancellationToken);

        if (uploadResult.IsFailure)
            return uploadResult.Error.ToResponse();

        return Ok(new FileUploadResponse(objectName, bucketName));
    }

    [HttpPost("get-url")]
    public async Task<IActionResult> GetDownloadUrl(
        [FromServices] DownloadFileHandler handler,
        [FromBody] DownloadFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var downloadResult = await handler.Download(request, cancellationToken);

        if (downloadResult.IsFailure)
            return downloadResult.Error.ToResponse();

        return Ok(downloadResult.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteFileHandler handler,
        [FromBody] DeleteFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var deleteResult = await handler.Delete(request, cancellationToken);

        if (deleteResult.IsFailure)
            return deleteResult.Error.ToResponse();

        return Ok();
    }
}
