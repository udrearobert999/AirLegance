using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;

namespace Application.Services;

class LocationsService : ILocationsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LocationsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IEnumerable<LocationDto>>> GetLocations()
    {
        var locations = await _unitOfWork.Location.GetAllAsync();

        var locationsResponse = _mapper.Map<IEnumerable<LocationDto>>(locations);

        return ResponseDto<IEnumerable<LocationDto>>.Success(locationsResponse);
    }
}