namespace Application.Dto
{
    public class FlightSearchRequestDto
    {
        public Guid SourceLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
    }
}
