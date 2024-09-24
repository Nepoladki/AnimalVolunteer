using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Features.Files.Delete;
using AnimalVolunteer.Application.Features.Files.GetUrl;
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

    public async Task<UnitResult<Error>> DeleteFile(
        DeleteFileRequest fileData, CancellationToken cancellationToken)
    {
        try
        {
            var fileExistsResult = await BucketExists(
                fileData.BucketName, cancellationToken);

            if (fileExistsResult.IsFailure)
                return fileExistsResult.Error;

            var objectExistsResult = await ObjectExists(
                fileData.BucketName, fileData.ObjectName, cancellationToken);

            if (objectExistsResult.IsFailure)
                return objectExistsResult.Error;

            var deletingArgs = new RemoveObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName);

            await _minioClient.RemoveObjectAsync(deletingArgs, cancellationToken);
            _logger.LogInformation(
                "Deleted minio object {object} from bucket {bucket}",
                fileData.ObjectName,
                fileData.BucketName);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete file from Minio");
            return Errors.Minio.DeleteFailure(fileData.ObjectName);
        }
    }

    public async Task<Result<string, Error>> GetFileUrl(
        DownloadFileRequest fileData, CancellationToken cancellationToken)
    {
        try
        {
            var fileExistsResult = await ObjectExists(
                fileData.BucketName, fileData.ObjectName, cancellationToken);

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

    public async Task<UnitResult<Error>> UploadFile(
        Stream file,
        string bucketName,
        string objectName, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            if ((await BucketExists(bucketName, cancellationToken)).IsFailure)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(file)
                .WithObjectSize(file.Length)
                .WithObject(objectName);

            var saveResult = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload file to Minio");
            return Error.Failure("file.upload", "Failed to upload file in Minio");
        }
    }

    public async Task<UnitResult<Error>> UploadFiles(
        IEnumerable<FileToUploadDto> petPhotos,
        string bucketName, 
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(_options.SemaphoreThreadLimit);

        try
        {
            if ((await BucketExists(bucketName, cancellationToken)).IsFailure)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            List<Task> tasks = [];

            foreach (var file in petPhotos)
            {
                await semaphoreSlim.WaitAsync(cancellationToken);

                var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(file.Content)
                .WithObjectSize(file.Content.Length)
                .WithObject(file.ObjectName);

                var task = _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                semaphoreSlim.Release();

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload file to Minio");
            return Error.Failure("file.upload", "Failed to upload file in Minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task<UnitResult<Error>> BucketExists(
        string bucketName, CancellationToken cancellationToken)
    {
        var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

        if (!await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken))
            return Errors.Minio.BucketNotFound(bucketName);

        return Result.Success<Error>();
    }

    private async Task<UnitResult<Error>> ObjectExists(
        string bucketName, string objectName, CancellationToken cancellationToken)
    {
        var statObjArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        var objStats = await _minioClient.StatObjectAsync(statObjArgs, cancellationToken);

        if (objStats is null)
            return Errors.Minio.ObjectNotFound(objectName);

        return Result.Success<Error>();
    }
}
