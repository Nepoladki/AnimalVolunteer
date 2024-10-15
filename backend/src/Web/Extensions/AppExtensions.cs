namespace AnimalVolunteer.API.Extensions;

public static class AppExtensions
{
    public static async Task<WebApplication> AddAutoMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        await dbContext.Database.MigrateAsync();

        return app;
    }
}
