using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;

namespace Application.Services;

class FlightsService : IFlightsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FlightsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IEnumerable<FlightSearchResponseDto>>> SearchFlight(
        FlightSearchRequestDto flightSearchRequestDto)
    {
        var flights = await _unitOfWork.Flights.GetFlights(flightSearchRequestDto.SourceLocationId,
            flightSearchRequestDto.DestinationLocationId);
        var response = _mapper.Map<IEnumerable<FlightSearchResponseDto>>(flights);

        return ResponseDto<IEnumerable<FlightSearchResponseDto>>.Success(response);
    }
}