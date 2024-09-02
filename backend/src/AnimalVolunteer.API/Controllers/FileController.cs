using AnimalVolunteer.Application.Features.Files.Upload;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace AnimalVolunteer.API.Controllers;

public class FileController : ApplicationController
{
    private readonly IMinioClient _minioClient;
    public FileController(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }
    [HttpPost]
    public async Task<IActionResult> UploadFile(
        [FromForm] IFormFile file,
        [FromServices] UploadFileHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var objectName = Guid.NewGuid().ToString();

    }
}
