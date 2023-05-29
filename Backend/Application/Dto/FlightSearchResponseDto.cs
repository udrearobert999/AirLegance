namespace Application.Dto
{
    public class FlightSearchResponseDto
    {
        public Guid Id { get; set; }
        public string Source { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public decimal Price { get; set; }
        public double Rating { get; set; }
    }
}