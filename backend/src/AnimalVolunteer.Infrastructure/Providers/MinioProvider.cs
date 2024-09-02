using AnimalVolunteer.Application.Features.Files.Delete;
using AnimalVolunteer.Application.Features.Files.GetUrl;
using AnimalVolunteer.Application.FileProvider;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Infrastructure.Options;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace AnimalVolunteer.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    private readonly MinioOptions _options;
    public MinioProvider(
        IMinioClient minioClient, 
        ILogger<MinioProvider> logger, 
        IOptionsSnapshot<MinioOptions> options)
    {
        _minioClient = minioClient;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<Result<bool, Error>> DeleteFile(DeleteFileRequest fileData, CancellationToken cancellationToken)
    {
        try
        {
            var fileExistsResult = await BucketAndFileExists(fileData.BucketName, fileData.ObjectName, cancellationToken);

            if (fileExistsResult.IsFailure)
                return fileExistsResult.Error;

            var deletingArgs = new RemoveObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName);

            await _minioClient.RemoveObjectAsync(deletingArgs, cancellationToken);
            _logger.LogInformation(
                "Deleted minio object {object} from bucket {bucket}",
                fileData.ObjectName,
                fileData.BucketName);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete file from Minio");
            return Errors.Minio.DeleteFailure(fileData.ObjectName);
        }
    }

    public async Task<Result<string, Error>> GetFileUrl(DownloadFileRequest fileData, CancellationToken cancellationToken)
    {
        try
        {
            var fileExistsResult = await BucketAndFileExists(fileData.BucketName, fileData.ObjectName, cancellationToken);

            if (fileExistsResult.IsFailure)
                return fileExistsResult.Error;

            var getObjArgs = new PresignedGetObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName)
                .WithExpiry(_options.UrlExpirySeconds);

            return await _minioClient.PresignedGetObjectAsync(getObjArgs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch file's url from Minio");
            return Errors.Minio.GetUrlFailure(fileData.ObjectName);
        }
    }

    public async Task<Result<string, Error>> UploadFile(
        UploadFileData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
            .WithBucket(fileData.BucketName);

            if (!await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken))
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(fileData.BucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            var path = Guid.NewGuid().ToString();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithStreamData(fileData.FileStream)
                .WithObjectSize(fileData.FileStream.Length)
                .WithObject(fileData.ObjectName);

            var saveResult = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return saveResult.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload file to Minio");
            return Error.Failure("file.upload", "Failed to upload file in Minio");
        }
    }

    private async Task<Result<bool, Error>> BucketAndFileExists(
        string bucketName, string objectName, CancellationToken cancellationToken)
    {

        var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

        if (!await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken))
            return Errors.Minio.BucketNotFound(bucketName);

        var statObjArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        var objStats = await _minioClient.StatObjectAsync(statObjArgs, cancellationToken);

        if (objStats is null)
            return Error.NotFound("File.NotFound","File does not exist in minio");

        return true;
    }
}
