using AnimalVolunteer.Core.DTOs.Discussions;
using LinqToDB;
using LinqToDB.Data;

namespace AnimalVolunteer.Discussions.Infrastructure.Linq2db;
public class Linq2dbConnection : DataConnection
{
    public Linq2dbConnection(DataOptions<Linq2dbConnection> options) 
        : base(options.Options)
    {
        
    }

    public ITable<DiscussionDto> Discussions =>
        this.GetTable<DiscussionDto>();

}
