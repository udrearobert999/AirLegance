using Application.Dto;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<GenericResponseDto> CreateUserAsync(UserRegistrationDto userRegistrationDto);
}