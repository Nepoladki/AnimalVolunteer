using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.API.Extensions;

public static class AppExtensions
{
    public static async Task<WebApplication> AddAutoMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        return app;
    }
}
