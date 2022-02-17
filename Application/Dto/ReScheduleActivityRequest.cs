namespace Application.Dto
{
    public  class ReScheduleActivityRequest
    {
        public Guid ActivityId { get; set; }

        public DateTimeOffset NewSchedule { get; set; }
    }
}
