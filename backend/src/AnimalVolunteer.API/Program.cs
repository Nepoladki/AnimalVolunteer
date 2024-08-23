using AnimalVolunteer.API;
using AnimalVolunteer.Application;
using AnimalVolunteer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

// Add other layers
services.AddPresentation()
    .AddApplication()
    .AddInfrastructure();

services.AddControllers(); 

// Swagger Generation
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
