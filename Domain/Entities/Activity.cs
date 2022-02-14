using Domain.Exceptions;

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

        public void Cancel()
        {
            Status = ActivityStatus.Cancelled;
        }

        public void ReSchedule(DateTime newSchedule)
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
        Active
    }
}
