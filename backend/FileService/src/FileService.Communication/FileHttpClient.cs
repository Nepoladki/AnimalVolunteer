using CSharpFunctionalExtensions;
using FileService.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace FileService.Communication;

public class FileHttpClient(HttpClient httpClient)
{
    public async Task<Result<IReadOnlyList<FileResponse>, string>> GetFilesPresignedUrls(
        GetFilesPresignedUrlsRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient
            .PostAsJsonAsync("files/presigned-urls", request, cancellationToken);
        if (response.StatusCode != HttpStatusCode.OK)
            return "Failed to get files presigned urls";

        var fileResponse = await response.Content
            .ReadFromJsonAsync<IEnumerable<FileResponse>>(cancellationToken);

        return fileResponse?.ToList() ?? [];
    }
}
