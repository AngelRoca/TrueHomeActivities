using Application.Contracts;
using Application.Dto;

namespace Application.Services.ListActivitiesFilters
{
    public class ListActivityFilterFactory
    {
        public static IListActivityFilter GetFilter(ListActiviesRequest request)
        {
            if (request.Status != null)
            {
                return new StatusListActivityFilter(request.Status.Value);
            }
            else if (request.StartDate != null && request.EndDate != null)
            {
                return new DateRangeListActivityFilter(request.StartDate.Value, request.EndDate.Value);
            }
            else
            {
                return new DefaultListActivityFilter();
            }
        }
    }
}
