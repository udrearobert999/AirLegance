using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILocationsService
    {
        public Task<ResponseDto<IEnumerable<LocationDto>>> GetLocations();
    }
}