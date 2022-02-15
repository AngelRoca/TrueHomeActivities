using Application.Contracts;
using Application.Dto;
using Application.Services.ListActivitiesFilters;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class ListActivities : IListActivities
    {
        private readonly IActivitiesRespository _activitiesRepo;

        public ListActivities(IActivitiesRespository activitiesRepo)
        {
            _activitiesRepo = activitiesRepo;
        }

        public async Task<IEnumerable<ActivityItem>> GetListAsync(ListActiviesRequest request)
        {
            IListActivityFilter filter = ListActivityFilterFactory.GetFilter(request);

            var activities = await _activitiesRepo.FindAllByPredicateAsync(filter.Predicate);

            return activities.Select(activity => BuildItem(activity));
        }

        private static ActivityItem BuildItem(Activity activity)
        {
            return new ActivityItem
            {
                ID = activity.Id.ToString(),
                Title = activity.Title,
                Schedule = activity.Schedule,
                Status = activity.Status.ToString(),
                Condition = EvaluateCondition(activity),
                CreatedAt = activity.CreatedAt,
                Property = new PropertyItem()
                {
                    ID = activity.PropertyId.ToString(),
                    Address = activity.Property.Address,
                    Title = activity.Property.Title
                }
            };
        }

        private static string EvaluateCondition(Activity activity)
        {
            if (activity.Status == ActivityStatus.Active &&
                activity.Schedule >= DateTime.Now)
            {
                return "Pendiente a realizar";
            }

            if (activity.Status == ActivityStatus.Active &&
                activity.Schedule < DateTime.Now)
            {
                return "Atrasada";
            }

            if (activity.Status == ActivityStatus.Done)
            {
                return "Finalizada";
            }

            return "NA";
        }
    }
}
