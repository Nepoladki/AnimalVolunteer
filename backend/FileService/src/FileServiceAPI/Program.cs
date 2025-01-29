using Amazon.S3;
using FileServiceAPI;
using FileServiceAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddSingleton<IAmazonS3>(_ =>
{
    var config = new AmazonS3Config
    {
        ServiceURL = "http://localhost:9000",
        ForcePathStyle = true,
        UseHttp = true
    };

    return new AmazonS3Client("minioadmin", "minioadmin", config);
});

services.AddLogging(config);

services.AddEndpoints();

var app = builder.Build();

app.MapEndpoints();

app.UseExceptionHandler();

app.Run();
