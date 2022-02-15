using Application.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services.ListActivitiesFilters
{
    public class DefaultListActivityFilter : IListActivityFilter
    {
        public Expression<Func<Activity, bool>> Predicate => activity =>
            activity.Schedule >= DateTime.Now.AddDays(-3) &&
            activity.Schedule <= DateTime.Now.AddDays(14);
    }
}
