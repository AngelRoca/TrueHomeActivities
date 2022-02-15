using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IActivitiesRespository
    {
        Task<Activity> InsertAsync(Activity activity);

        Task<IEnumerable<Activity>> FindAllByPropertyIdAsync(Guid propertyId);

        Task<Activity> FindByIdAsync(Guid activityId);

        Task<Activity> UpdateAsync(Activity activity);

        Task<IEnumerable<Activity>> FindAllByPredicateAsync(Expression<Func<Activity, bool>> predicate);
    }
}
