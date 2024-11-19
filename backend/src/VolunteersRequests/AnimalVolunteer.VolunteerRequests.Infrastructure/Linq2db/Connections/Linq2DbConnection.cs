using AnimalVolunteer.Core.Options;
using AnimalVolunteer.VolunteerRequests.Domain;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Linq2db.Connections;

public class Linq2DbConnection : DataConnection
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;
    private readonly IConfiguration _configuration;
    public Linq2DbConnection(
        IOptions<DatabaseOptions> databaseOptions, 
        IConfiguration configuration)
        : base(new DataOptions().UsePostgreSQL())
    {
        _databaseOptions = databaseOptions;
        _configuration = configuration;
    }

    public ITable<VolunteerRequest> VolunteerRequests => this.GetTable<VolunteerRequest>();
}
