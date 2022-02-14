using Application.Contracts;
using Application.Dto;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class ReScheduleActivity : IReScheduleActivity
    {
        private readonly IActivitiesRespository _activitiesRepo;

        public ReScheduleActivity(
            IActivitiesRespository activitiesRepo
            )
        {
            _activitiesRepo = activitiesRepo;
        }

        public async Task<ReScheduleActivityResponse> ReScheduleAync(ReScheduleActivityRequest request)
        {
            var activity = await _activitiesRepo.FindByIdAsync(request.ActivityId);

            if (activity.Status == ActivityStatus.Cancelled)
            {
                throw new CancelledActivityReScheduleException();
            }

            activity.Schedule = request.NewSchedule;

            await _activitiesRepo.UpdateAsync(activity);

            var response = new ReScheduleActivityResponse() { Success = true };

            return response;
        }

    }
}
