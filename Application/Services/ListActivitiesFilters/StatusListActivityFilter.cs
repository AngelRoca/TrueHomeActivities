using Application.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services.ListActivitiesFilters
{
    public class StatusListActivityFilter : IListActivityFilter
    {
        private readonly ActivityStatus _status;

        public StatusListActivityFilter(ActivityStatus status)
        {
            _status = status;
        }

        public Expression<Func<Activity, bool>> Predicate => activity => activity.Status == _status;
    }
}
