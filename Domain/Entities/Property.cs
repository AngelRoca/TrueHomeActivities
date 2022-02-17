namespace Domain.Entities
{
    public class Property
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public DateTimeOffset? DisabledAt { get; set; }

        public PropertyStatus Status { get; set; }
    }

    public enum PropertyStatus
    {
        Disabled,
        Active,
        Sold
    }
}
