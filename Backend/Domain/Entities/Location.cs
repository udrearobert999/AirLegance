namespace Domain.Entities
{
    public class Location: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;

        public ICollection<Flight> SourceFlights { get; set; } = null!;
        public ICollection<Flight> DestinationFlights { get; set; } = null!;
    }
}
