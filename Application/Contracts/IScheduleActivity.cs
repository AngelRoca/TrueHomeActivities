using Application.Dto;

namespace Application.Contracts
{
    public interface IScheduleActivity
    {
        Task<ScheduledActivity> ScheduleAsync(ScheduleActivityRequest request);
    }
}