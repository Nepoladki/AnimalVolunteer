﻿namespace AnimalVolunteer.Core.Options;

public class MinioOptions
{
    public const string SECTION_NAME = "Minio";
    public string Endpoint { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool EnableSsl { get; init; } = false;
    public int UrlExpirySeconds { get; init; }
    public string BucketName { get; init; } = string.Empty;
    public int SemaphoreThreadLimit { get; init; }
}
