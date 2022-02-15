namespace Domain.Entities
{
    public class Property
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DisabledAt { get; set; }

        public PropertyStatus Status { get; set; }
    }

    public enum PropertyStatus
    {
        Disabled,
        Active,
        Sold
    }
}
