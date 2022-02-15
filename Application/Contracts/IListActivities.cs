using Application.Dto;

namespace Application.Contracts
{
    public interface IListActivities
    {
        Task<IEnumerable<ActivityItem>> GetListAsync(ListActiviesRequest filter);
    }
}
