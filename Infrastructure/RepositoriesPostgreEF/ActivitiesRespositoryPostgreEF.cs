using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.RepositoriesPostgreEF
{
    public class ActivitiesRespositoryPostgreEF : IActivitiesRespository
    {
        private readonly TrueHomeDataContext _dbContext;

        public ActivitiesRespositoryPostgreEF(TrueHomeDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Activity>> FindAllByPredicateAsync(Expression<Func<Activity, bool>> predicate)
        {
            return await _dbContext
                .Activities
                .Include(a => a.Property)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> FindAllByPropertyIdAsync(Guid propertyId)
        {
            return await _dbContext.Activities.Where(a => a.PropertyId == propertyId).ToListAsync();
        }

        public async Task<Activity> FindByIdAsync(Guid activityId)
        {
            return await _dbContext.Activities.FindAsync(activityId);
        }

        public async Task<Activity> InsertAsync(Activity activity)
        {
            activity.CreatedAt = DateTimeOffset.Now;
            activity.UpdatedAt = DateTimeOffset.Now;

            await _dbContext.Activities.AddAsync(activity);
            int result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return activity;
            }

            return new Activity();
        }

        public async Task<Activity> UpdateAsync(Activity activity)
        {
            activity.UpdatedAt = DateTimeOffset.Now;

            int result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return activity;
            }

            return new Activity();
        }
    }
}
