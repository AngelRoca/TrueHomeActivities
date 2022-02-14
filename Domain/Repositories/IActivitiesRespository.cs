using Domain.Entities;

namespace Domain.Repositories
{
    public interface IActivitiesRespository
    {
        Task<Activity> InsertAsync(Activity activity);

        Task<IEnumerable<Activity>> FindAllByPropertyIdAsync(Guid propertyId);
    }
}
