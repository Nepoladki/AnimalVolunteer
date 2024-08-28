using AnimalVolunteer.API;
using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.API.Middlewares;
using AnimalVolunteer.Application;
using AnimalVolunteer.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var config = builder.Configuration;

// Add other layers
services.AddApi(config)
    .AddApplication()
    .AddInfrastructure();

services.AddControllers();

// Swagger Generation
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Auto-migrations
    await app.AddAutoMigrations();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
