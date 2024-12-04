using System.Linq.Expressions;

namespace AnimalVolunteer.Core.QueriesExtensions;

public static class QueriesExensions
{
    public static IQueryable<T> WhereIf<T>(
    this IQueryable<T> source,
    bool condition,
    Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}

