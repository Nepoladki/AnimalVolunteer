using AnimalVolunteer.API;
using AnimalVolunteer.API.Middlewares;
using AnimalVolunteer.Species.Web;
using AnimalVolunteer.Volunteers.Web;
using AnimalVolunteer.Accounts.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

// Add other layers
services
    .AddApi(config)
    .AddAccountsModule(config)
    .AddVolunteersModule(config)
    .AddSpeciesModule(config);

services.AddControllers();

// Swagger Generation moved to DependencyInjection.cs

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
