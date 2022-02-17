using Domain.Exceptions;

namespace Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }

        public DateTimeOffset Schedule { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public ActivityStatus Status { get; set; }

        public Property Property { get; set; }

        public void Cancel()
        {
            Status = ActivityStatus.Cancelled;
        }

        public void ReSchedule(DateTimeOffset newSchedule)
        {
            if (Status == ActivityStatus.Cancelled)
            {
                throw new CancelledActivityReScheduleException();
            }

            Schedule = newSchedule;
        }
    }

    public enum ActivityStatus
    {
        Cancelled,
        Active,
        Done
    }
}
