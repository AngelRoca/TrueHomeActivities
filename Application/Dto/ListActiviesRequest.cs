using Domain.Entities;

namespace Application.Dto
{
    public class ListActiviesRequest
    {
        public ActivityStatus? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
