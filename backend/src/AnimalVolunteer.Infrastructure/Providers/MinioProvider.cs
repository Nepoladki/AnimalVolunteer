using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
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
        FileInfoDto fileInfo, CancellationToken cancellationToken)
    {
        try
        {
            var deletingArgs = new RemoveObjectArgs()
                .WithBucket(fileInfo.BucketName)
                .WithObject(fileInfo.ObjectName);

            await _minioClient.RemoveObjectAsync(deletingArgs, cancellationToken);
            _logger.LogInformation(
                "Deleted minio object {object} from bucket {bucket}",
                fileInfo.ObjectName,
                fileInfo.BucketName);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete file from Minio");
            return Errors.Minio.DeleteFailure(fileInfo.ObjectName);
        }
    }

    public async Task<Result<string, Error>> GetFileUrl(
        FileInfoDto fileInfo, CancellationToken cancellationToken)
    {
        try
        {
            var fileExistsResult = await ObjectExists(
                fileInfo.BucketName, fileInfo.ObjectName, cancellationToken);

            if (fileExistsResult.IsFailure)
                return fileExistsResult.Error;

            var getObjArgs = new PresignedGetObjectArgs()
                .WithBucket(fileInfo.BucketName)
                .WithObject(fileInfo.ObjectName)
                .WithExpiry(_options.UrlExpirySeconds);

            return await _minioClient.PresignedGetObjectAsync(getObjArgs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch file's url from Minio");
            return Errors.Minio.GetUrlFailure(fileInfo.ObjectName);
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
            await CreateBucketIfBucketDoesntExist(bucketName, cancellationToken);

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

    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<UploadingFileDto> files,
        string bucketName, 
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(_options.SemaphoreThreadLimit);

        try
        {
            await CreateBucketIfBucketDoesntExist(bucketName, cancellationToken);

            var tasks = files
                .Select(async file => await PutObject(
                    file, bucketName, semaphoreSlim, cancellationToken));

            var allPathsResult = await Task.WhenAll(tasks);

            if (allPathsResult.Any(x => x.IsFailure))
                return allPathsResult.First().Error;

            var allPaths = allPathsResult.Select(x => x.Value).ToList();

            _logger.LogInformation("Uploaded files: {files}", allPaths.Select(f => f.Value));

            return allPaths;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, 
                "Failed to upload file to Minio, files amount: {amount}", 
                files.Count());
            return Error.Failure("file.upload", "Failed to upload file in Minio");
        }
    }

    private async Task<Result<FilePath, Error>> PutObject(
        UploadingFileDto fileData,
        string bucketName,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithStreamData(fileData.Content)
            .WithObjectSize(fileData.Content.Length)
            .WithObject(fileData.FilePath.Value);

        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FilePath.Value,
                bucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task CreateBucketIfBucketDoesntExist(
        IEnumerable<string> buckets, CancellationToken cancellationToken)
    {
        HashSet<string > bucketNames = [..buckets];

        foreach (var bucketName in bucketNames)
        {
            if (!await BucketExists(bucketName, cancellationToken))
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    private async Task CreateBucketIfBucketDoesntExist(
       string bucketName, CancellationToken cancellationToken)
    {
        if (!await BucketExists(bucketName, cancellationToken))
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }
    }

    private async Task<bool> BucketExists(
        string bucketName, CancellationToken cancellationToken)
    {
        var bucketExistArgs = new BucketExistsArgs()
            .WithBucket(bucketName);

        return await _minioClient
            .BucketExistsAsync(bucketExistArgs, cancellationToken);
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
