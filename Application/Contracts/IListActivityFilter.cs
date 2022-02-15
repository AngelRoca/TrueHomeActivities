using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Contracts
{
    public interface IListActivityFilter
    {
        Expression<Func<Activity, bool>> Predicate { get; }
    }
}
