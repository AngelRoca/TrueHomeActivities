namespace Application.Dto
{
    public class ActivityItem
    {
        public String ID { get; set; }

        public DateTime Schedule { get; set; }

        public String Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }

        public string Condition { get; set; }

        public PropertyItem Property { get; set; }
    }
}
