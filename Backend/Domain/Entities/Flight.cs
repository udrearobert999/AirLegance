namespace Domain.Entities
{
    public class Flight: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid SourceLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }

        public Location SourceLocation { get; set; } = null!;
        public Location DestinationLocation { get; set; } = null!;
    }
}