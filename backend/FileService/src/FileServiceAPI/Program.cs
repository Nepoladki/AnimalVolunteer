using Amazon.S3;
using FileServiceAPI;
using FileServiceAPI.Endpoints;
using FileServiceAPI.Infrastructure;
using FileServiceAPI.Interfaces;
using FileServiceAPI.MongoDataAccess;
using FileServiceAPI.Options;
using Hangfire;
using Hangfire.PostgreSql;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddSingleton<IMongoClient>(
    new MongoClient(config.GetConnectionString(MongoDbOptions.SECTION_NAME)));

services.AddScoped<FilesMongoDbContext>();
services.AddScoped<IFilesRepository, FilesRepository>();

services.AddHangfire(cfg => cfg
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(config.GetConnectionString("Hangfire-postgres")
)));

services.AddHangfireServer();

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
services.AddExceptionHandler<GlobalExceptionHandler>();
services.AddProblemDetails();

services.AddEndpoints();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

app.UseExceptionHandler();

app.UseHangfireDashboard();

app.Run();
