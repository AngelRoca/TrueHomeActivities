namespace Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }

        public DateTime Schedule { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ActivityStatus Status { get; set; }
    }

    public enum ActivityStatus
    {
        Cancelled,
        Active
    }
}
