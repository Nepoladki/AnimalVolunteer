using AnimalVolunteer.API;
using AnimalVolunteer.API.Middlewares;
using AnimalVolunteer.Species.Web;
using AnimalVolunteer.Volunteers.Web;
using AnimalVolunteer.Accounts.Web;
using Serilog;
using AnimalVolunteer.Accounts.Infrastructure.DatabaseSeeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AnimalVolunteer.VolunteerRequests.Web;
using AnimalVolunteer.Discussions.Web;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

// Add other layers
services
    .AddApi(config)
    .AddAccountsModule(config)
    .AddVolunteersModule(config)
    .AddSpeciesModule(config)
    .AddVolunteerRequestsModule(config)
    .AddDiscussionsModule(config);

services.AddControllers();

// Swagger Generation moved to DependencyInjection.cs

var app = builder.Build();

var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();
await accountsSeeder.SeedAsync();

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
