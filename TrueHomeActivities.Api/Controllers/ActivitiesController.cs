using Application.Contracts;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TrueHomeActivities.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IListActivities _listActivities;
        private readonly ICancelActivity _cancelActivity;
        private readonly IReScheduleActivity _reScheduleActivity;
        private readonly IScheduleActivity _scheduleActivity;

        public ActivitiesController(
            IListActivities listActivities,
            ICancelActivity cancelActivity,
            IReScheduleActivity reScheduleActivity,
            IScheduleActivity scheduleActivity
            )
        {
            _listActivities = listActivities;
            _cancelActivity = cancelActivity;
            _reScheduleActivity = reScheduleActivity;
            _scheduleActivity = scheduleActivity;
        }

        [HttpGet]
        public async Task<IEnumerable<ActivityItem>> ListActivities([FromQuery]ListActiviesRequest request)
        {
            return await _listActivities.GetListAsync(request);
        }

        [HttpPost]
        public async Task<ScheduledActivity> ScheduleActivity([FromBody] ScheduleActivityRequest request)
        {
            return await _scheduleActivity.ScheduleAsync(request);
        }

        [HttpPut]
        public async Task<ReScheduleActivityResponse> ReScheduleActivity([FromBody] ReScheduleActivityRequest request)
        {
            return await _reScheduleActivity.ReScheduleAync(request);
        }

        [HttpDelete]
        public async Task<CancelActivityResponse> CancelActivity([FromBody] CancelActivityRequest request)
        {
            return await _cancelActivity.CancelAsync(request);
        }
    }
}
