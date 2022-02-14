namespace Application.Dto
{
    public class ScheduleActivityRequest
    {
        public Guid PropertyId { get; set; }

        public DateTime Schedule { get; set; }

        public string Title { get; set; }
    }
}
