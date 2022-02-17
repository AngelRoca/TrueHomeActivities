using Application.Contracts;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TrueHomeActivities.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IListActivities _listActivities;
        private readonly ICancelActivity _cancelActivity;
        private readonly IReScheduleActivity _reScheduleActivity;
        private readonly IScheduleActivity _scheduleActivity;

        public ActivityController(
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

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ActivityItem>>> ListActivities([FromQuery]ListActiviesRequest request)
        {
            var response = await _listActivities.GetListAsync(request);

            return Ok(response);
        }

        [HttpPost("Schedule")]
        [Produces(typeof(ScheduledActivity))]
        public async Task<IResult> ScheduleActivity([FromBody] ScheduleActivityRequest request)
        {
            var response = await _scheduleActivity.ScheduleAsync(request);

            return Results.Ok(response);
        }

        [HttpPatch("ReSchedule")]
        [Produces(typeof(ReScheduleActivityResponse))]
        public async Task<IResult> ReScheduleActivity([FromBody] ReScheduleActivityRequest request)
        {
            var response = await _reScheduleActivity.ReScheduleAync(request);

            return Results.Ok(response);
        }

        [HttpDelete("Cancel")]
        public async Task<ActionResult<CancelActivityResponse>> CancelActivity([FromBody] CancelActivityRequest request)
        {
            var response = await _cancelActivity.CancelAsync(request);
            
            return Ok(response);
        }
    }
}
