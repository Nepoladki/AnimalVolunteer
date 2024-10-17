using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.API.Extensions;

public static class AppExtensions
{
    public static async Task<WebApplication> AddAutoMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var speciesDbContext = scope.ServiceProvider.GetRequiredService<Species.Infrastructure.DbContexts.WriteDbContext>();
        var volunteersDbContext = scope.ServiceProvider.GetRequiredService<Volunteers.Infrastructure.DbContexts.WriteDbContext>();
        await speciesDbContext.Database.MigrateAsync();
        await volunteersDbContext.Database.MigrateAsync();

        return app;
    }
}
