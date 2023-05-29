namespace Domain.Entities
{
    public class FlightSchedule: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property
        public Flight Flight { get; set; } = null!;
        public User User { get; set; } = null;
    }
}
