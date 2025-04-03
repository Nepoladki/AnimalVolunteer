namespace FileServiceAPI.Options;

public class MinioOptions
{
    public const string SECTION_NAME = "minio";

    public string Endpoint { get; init; } = string.Empty;
    public string AccessKey { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public bool WithSsl { get; init; }
}
