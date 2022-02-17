using Application.Contracts;
using Application.Dto;
using Domain.Repositories;

namespace Application.Services
{
    public class CancelActivity : ICancelActivity
    {
        private readonly IActivitiesRespository _activitiesRepo;

        public CancelActivity(IActivitiesRespository activitiesRepo)
        {
            _activitiesRepo = activitiesRepo;
        }

        public async Task<CancelActivityResponse> CancelAsync(CancelActivityRequest request)
        {
            var activity = await _activitiesRepo.FindByIdAsync(request.ActivityId);

            activity.Cancel();

            await _activitiesRepo.UpdateAsync(activity);

            return new CancelActivityResponse() { Success = true };
        }
    }
}
