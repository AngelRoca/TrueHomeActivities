namespace Application.Dto
{
    public  class ReScheduleActivityRequest
    {
        public Guid ActivityId { get; set; }

        public DateTime NewSchedule { get; set; }
    }
}
