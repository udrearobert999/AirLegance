using Application.Dto;

namespace Application.Interfaces
{
    public interface IFlightsService
    {
        public Task<ResponseDto<IEnumerable<FlightSearchResponseDto>>> SearchFlight(FlightSearchRequestDto flightSearchRequestDto);
    }
}
