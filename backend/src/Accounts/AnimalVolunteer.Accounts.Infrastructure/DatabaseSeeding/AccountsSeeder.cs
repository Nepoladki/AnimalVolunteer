using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Accounts.Infrastructure.DatabaseSeeding;


public class AccountsSeeder
{
    private readonly IServiceScopeFactory _scopeFactory;
    public AccountsSeeder(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task SeedAsync()
    {
        using var scope = _scopeFactory.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<AccountsSeederService>();
        
        await service.SeedAsync();
    }
}
 