using Application.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services.ListActivitiesFilters
{
    public class DateRangeListActivityFilter : IListActivityFilter
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public DateRangeListActivityFilter(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public Expression<Func<Activity, bool>> Predicate => activity => 
            activity.Schedule >= _startDate &&
            activity.Schedule <= _endDate;
    }
}
