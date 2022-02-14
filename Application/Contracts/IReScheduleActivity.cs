using Application.Dto;

namespace Application.Contracts
{
    public interface IReScheduleActivity
    {
        Task<ReScheduleActivityResponse> ReScheduleAync(ReScheduleActivityRequest request);
    }
}
